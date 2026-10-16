using Renci.SshNet;
using SshNet.Agent;
using System.ComponentModel;
using System.Text.Json;

namespace SSH_Tunnel_Manager
{
    public partial class SSHTunnelManager : Form
    {
        SshAgentPrivateKey[] privateKeys = [];
        internal SshClient? client = null;

        public SSHTunnelManager()
        {
            InitializeComponent();
        }

        private void SSHTunnelManager_Load(object sender, EventArgs e)
        {
            // Get the SSH Keys
            Pageant agent = new();
            try
            {
                privateKeys = agent.RequestIdentities();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            if (privateKeys.Length == 0)
            {
                MessageBox.Show("No keys loaded in Pageant. Exiting!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            // Set up configured tunnel BindingList
            tunnelList.DataSource = new BindingList<TunnelDefinition>();
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Tunnels))
            {
                try
                {
                    var jsonTunnels = JsonSerializer.Deserialize<List<Dictionary<string, dynamic>>>(Properties.Settings.Default.Tunnels) ?? throw new Exception("Invalid Tunnels JSON Config");
                    foreach (Dictionary<string, dynamic> thisTunnel in jsonTunnels)
                    {
                        ((BindingList<TunnelDefinition>)tunnelList.DataSource).Add(new TunnelDefinition(this, thisTunnel["Host"].ToString(), thisTunnel["LocalPort"].GetUInt32(), thisTunnel["RemotePort"].GetUInt32(), thisTunnel["Label"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tunnelList.DataSource = new BindingList<TunnelDefinition>();
                }
            }

            double scaleFactor = this.DeviceDpi / 96.0;
            tunnelList.Columns["Enable"].Width = (int)(50 * scaleFactor);
            tunnelList.Columns["Host"].Width = (int)(100 * scaleFactor);
            tunnelList.Columns["LocalPort"].Width = (int)(90 * scaleFactor);
            tunnelList.Columns["RemotePort"].Width = (int)(100 * scaleFactor);
            tunnelList.Columns["LocalPort"].HeaderText = "Local Port";
            tunnelList.Columns["RemotePort"].HeaderText = "Remote Port";
            tunnelList.Columns["Label"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Set up delete column
            DataGridViewButtonColumn deleteColumn = new()
            {
                Name = "DeleteColumn",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = (int)(70 * scaleFactor)
            };
            tunnelList.Columns.Add(deleteColumn);

            // Load Settings
            hostBox.Text = Properties.Settings.Default.Host;
            usernameBox.Text = Properties.Settings.Default.Username;
        }

        private void addConnection_Click(object sender, EventArgs e)
        {
            ((BindingList<TunnelDefinition>)tunnelList.DataSource).Add(new TunnelDefinition(this));
        }

        private void tunnelList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (tunnelList.IsCurrentCellDirty && tunnelList.CurrentCell is DataGridViewCheckBoxCell)
            {
                tunnelList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void tunnelList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == tunnelList.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                ((BindingList<TunnelDefinition>)tunnelList.DataSource)[e.RowIndex].Enable = false;
                ((BindingList<TunnelDefinition>)tunnelList.DataSource).RemoveAt(e.RowIndex);
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            // Save settings
            Properties.Settings.Default.Host = hostBox.Text;
            Properties.Settings.Default.Username = usernameBox.Text;
            Properties.Settings.Default.Tunnels = JsonSerializer.Serialize((BindingList<TunnelDefinition>)tunnelList.DataSource);
            Properties.Settings.Default.Save();

            // Connect to SSH
            if (client == null)
            {
                connectButton.Enabled = false;
                hostBox.Enabled = false;
                usernameBox.Enabled = false;

                // Connect to server
                try
                {
                    client = new(hostBox.Text, usernameBox.Text, privateKeys);
                    client.Connect();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    client = null;
                    hostBox.Enabled = true;
                    usernameBox.Enabled = true;
                    connectButton.Enabled = true;
                    return;
                }

                // Reset UI
                connectButton.Text = "Disconnect";
                connectButton.Enabled = true;
            }

            // Disconnect from SSH
            else
            {
                // Shut down all tunnels
                foreach (TunnelDefinition thisDef in ((BindingList<TunnelDefinition>)tunnelList.DataSource))
                {
                    thisDef.Enable = false;
                }

                // Disconnect from SSH
                tunnelList.Refresh();
                client.Disconnect();
                client.Dispose();
                client = null;

                // Reset UI
                connectButton.Text = "Connect";
                hostBox.Enabled = true;
                usernameBox.Enabled = true;
            }
        }

        private void tunnelList_SelectionChanged(object sender, EventArgs e)
        {
            tunnelList.ClearSelection();
        }
    }
}
