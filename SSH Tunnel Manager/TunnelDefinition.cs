using Renci.SshNet;
using System.Text.Json.Serialization;

namespace SSH_Tunnel_Manager
{
    internal class TunnelDefinition
    {
        [JsonIgnore] bool _enable;
        [JsonIgnore] ForwardedPortLocal? obj = null;
        [JsonIgnore] readonly SSHTunnelManager manager;

        [JsonIgnore] public bool Enable
        {
            get
            {
                return _enable;
            }
            set
            {
                if(obj != null && !value)
                {
                    obj.Stop();
                    manager.client?.RemoveForwardedPort(obj);
                    obj = null;
                }

                if (obj == null && value)
                {
                    if (manager.client == null)
                    {
                        MessageBox.Show("Connect to an SSH host first", manager.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        manager.tunnelList.RefreshEdit();
                        return;
                    }

                    if (String.IsNullOrWhiteSpace(Host))
                    {
                        MessageBox.Show("Host cannot be blank", manager.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        manager.tunnelList.RefreshEdit();
                        return;
                    }

                    try
                    {
                        obj = new ForwardedPortLocal("127.0.0.1", LocalPort, Host, RemotePort);
                        manager.client?.AddForwardedPort(obj);
                        obj.Start();
                    }
                    catch (Exception ex)
                    {
                        obj?.Stop();
                        obj = null;
                        MessageBox.Show(ex.Message, manager.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        manager.tunnelList.RefreshEdit();
                        return;
                    }
                }

                _enable = value;
            }
        }

        public string Host { get; set; }
        public uint LocalPort { get; set; }
        public uint RemotePort { get; set; }
        public string Label { get; set; }

        public TunnelDefinition(SSHTunnelManager manager, string host = "", uint localPort = 22, uint remotePort = 22, string label = "")
        {
            this.manager = manager;
            Enable = false;
            Host = host;
            LocalPort = localPort;
            RemotePort = remotePort;
            Label = label;
        }


        ~TunnelDefinition()
        {
            if(obj != null)
            {
                obj.Stop();
                manager.client?.RemoveForwardedPort(obj);
            }
        }
    }
}
