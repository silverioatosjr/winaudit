using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinAudit
{
    public class GetInformation
    {
        string group = string.Empty;
        string[] propArray;
        int inputIndex = 3;
        public string ShowInfo(string techName, string client, string site, string processor, string pc, string os){
            var i = 0;
            string scanDate = DateTime.Now.ToString("MM/d/yyyy HH:mm:ss");
            
            var macAddr = (
                        from nic in NetworkInterface.GetAllNetworkInterfaces()
                        where nic.OperationalStatus == OperationalStatus.Up
                        select nic.GetPhysicalAddress().ToString()).FirstOrDefault();

            var htm = @"<div class=well><div><span>Technician: </span><input style='border:none;' name='techname' value='" + techName + "'/></div><div><span>Client: </span><input style='border:none;' name='client' value='" + client + "'/></div><div><span>Site: </span><input style='border:none;' name='site' value='" + site + "'/></div><div><span>Processor: </span><input style='border:none;' name='processor' value='" + processor + "'/></div><div><span>Workstation: </span><input style='border:none;' name='pc' value='" + pc + "'/></div><div><span>Operating System: </span><input style='border:none;' name='os' value='" + os + "'/></div><div><span>Scanned Date: </span><input style='border:none;' name='scandate' value='" + scanDate + "'/></div><div><span>Mac Address: </span><input style='border:none;' name='mac' value='" + macAddr + "'/></div></div>" 
                + @"<table id=first class='table-bordered'><th colspan=5><center>System Information</center></th> <tr><td><input style='border:none;' name='machinename1' value='" + Environment.MachineName + "'/></td><td><input style='border:none;' name='group1' value= 'SystemInfo'/></td><td><input style='border:none;' name='dgroup1' value='System Overview'/> </td><td><input style='border:none;' name='sequence1' value='" + ++i + "'/></td><td><input style='border:none;' name='property1' value='Computer Name'/> </td><td><input style='border:none;' name='value1' value='" + Environment.MachineName
             + @"'/></td><td><input style='border:none;' name='scanDate1' value= '" + scanDate + @"'/></td><td><input style='border:none;' name='macAddress1' value= '" + macAddr + @"'/></td></tr>
               <tr><td><input style='border:none;' name='machinename2' value='" + Environment.MachineName + "'/></td><td><input style='border:none;' name='group2' value= 'SystemInfo'/></td><td><input style='border:none;' name='dgroup2' value='System Overview'/> </td><td><input style='border:none;' name='sequence2' value='" + ++i + "'/></td><td><input style='border:none;' name='property2' value='User Name'/> </td><td><input style='border:none;' name='value2' value='" + Environment.UserName
             + @"'/></td><td><input style='border:none;' name='scanDate2' value= '" + scanDate + @"'/></td><td><input style='border:none;' name='macAddress2' value= '" + macAddr + @"'/></td></tr>";
           
            group = "Win32_ComputerSystemProduct";
            propArray = new string[] { "Vendor", "Version", "Name", "IdentifyingNumber" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "System Overview", propArray, scanDate, macAddr);
            group = "Win32_OperatingSystem";
            propArray = new string[] { "Caption" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "System Overview", propArray, scanDate, macAddr);
            group = "Win32_BIOS";
            propArray = new string[] { "Version" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "System Overview", propArray, scanDate, macAddr);
            group = "Win32_Processor";
            propArray = new string[] { "Name" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "System Overview", propArray, scanDate, macAddr);
            group = "Win32_DiskDrive";
            propArray = new string[] { "Size" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "System Overview", propArray, scanDate, macAddr);
            group = "Win32_PhysicalMemory";
            propArray = new string[] { "Capacity" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "System Overview", propArray, scanDate, macAddr);
            group = "Win32_LocalTime";
            propArray = new string[] { "Year", "Month", "Day", "Hour", "Minute", "Second" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "System Overview", propArray, scanDate, macAddr);
            group = "Win32_OperatingSystem";
            propArray = new string[] { "Caption", "BuildNumber", "InstallDate", "SerialNumber", "OSArchitecture", "RegisteredUser", "WindowsDirectory", "SystemDirectory" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Operating System", propArray, scanDate, macAddr);
            group = "Win32_Printer";
            propArray = new string[] { "Name", "HorizontalResolution", "VerticalResolution", "DriverName" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Printer", propArray, scanDate, macAddr);
            group = "Win32_PrinterConfiguration";
            propArray = new string[] { "Name", "PaperSize", "Orientation" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Printer Configuration", propArray, scanDate, macAddr);

            ////bios version ok checked
            ////group = "Win32_BIOS";
            ////propArray = new string[] { "Manufacturer", "ReleaseDate", "SMBIOSBIOSVersion", "Version" };
            ////htm += SaveAllDeviceInfo(Environment.MachineName, group, "BIOS Version", propArray, scanDate.ToLocalTime().ToString(), macAddr);

            //// processor ok checked
            group = "Win32_Processor";
            propArray = new string[] { "Name", "CurrentClockSpeed", "Manufacturer", "NumberOfCores", "NumberOfLogicalProcessors" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Processor", propArray, scanDate, macAddr);

            ////useraccount ok checked
            group = "Win32_UserAccount";
            propArray = new string[] { "Name", "FullName", "Description", "PasswordChangeable", "PasswordExpires", "PasswordRequired", "Disabled" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Users", propArray, scanDate, macAddr);

            ////display ok checked
            group = "Win32_VideoController";
            propArray = new string[] { "AdapterDACType", "VideoProcessor" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Display Adapters", propArray, scanDate, macAddr);
            group = "Win32_DisplayConfiguration";
            propArray = new string[] { "DeviceName", "BitsPerPel", "DisplayFrequency", "PelsHeight", "PelsWidth" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Display Adapters", propArray, scanDate, macAddr);
            group = "Win32_DisplayControllerConfiguration";
            propArray = new string[] { "RefreshRate", "VerticalResolution", "VideoMode" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Display Adapters", propArray, scanDate, macAddr);

            ////PHysical disk ok checked
            group = "Win32_DiskDrive";
            propArray = new string[] { "Model", "Description", "Index", "FirmwareRevision", "SerialNumber", "MediaType", "TotalCylinders", "TotalHeads", "SectorsPerTrack", "PNPDeviceID", "Size" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Physical Disk", propArray, scanDate, macAddr);

            ////Logical disk ok checked
            group = "Win32_LogicalDisk";
            propArray = new string[] { "Name", "Description", "VolumeSerialNumber", "Size", "FreeSpace", "FileSystem" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Logical Disk", propArray, scanDate, macAddr);

            //Installed Software ok checked
            //group = "Win32_Product";
            //propArray = new string[] { "Name", "Vendor", "Version", "InstallDate" };
            //htm += SaveAllDeviceInfo(Environment.MachineName, group, "Installed Software", propArray, scanDate.ToLocalTime().ToString(), macAddr);

            ////Startup Programs ok checked
            ////group = "Win32_StartupCommand";
            ////propArray = new string[] { "Name", "Command", "Location" };
            ////htm += SaveAllDeviceInfo(Environment.MachineName, group, "Startup Programs", propArray, scanDate.ToLocalTime().ToString(), macAddr);

            ////network routing table ok checked
            //group = "Win32_IP4RouteTable";
            //propArray = new string[] { "Destination", "Mask", "NextHop", "Metric1", "Age", "Protocol" };
            //SaveAllDeviceInfo(Environment.MachineName, group, "Routing table", propArray, scanDate.ToLocalTime().ToString(), macAddr);

            ////Group users ok
            group = "Win32_Group";
            propArray = new string[] { "Name", "Description", "LocalAccount" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Group Users", propArray, scanDate, macAddr);

            ////Peripherals ok checked
            group = "Win32_Keyboard";
            propArray = new string[] { "Name" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Keyboard", propArray, scanDate, macAddr);

            ////Network adapters ok checked
            group = "Win32_NetworkAdapterConfiguration";
            propArray = new string[] { "DHCPLeaseExpires", "DHCPLeaseObtained", "Description", "DHCPServer", "DNSDomain", "DNSHostName", "MACAddress" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Network Adapter", propArray, scanDate, macAddr);


            ////Memory
            group = "Win32_PhysicalMemory";
            propArray = new string[] { "BankLabel", "Capacity", "Manufacturer" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Memory", propArray, scanDate, macAddr);
            group = "Win32_OperatingSystem";
            propArray = new string[] { "FreePhysicalMemory" };
            htm += SaveAllDeviceInfo(Environment.MachineName, group, "Memory", propArray, scanDate, macAddr);

            //cache memory
            //group = "Win32_CacheMemory";
            //propArray = new string[] { "Purpose", "MaxCacheSize", "InstalledSize" };
            //SaveAllDeviceInfo(Environment.MachineName, group, "Cache", propArray, scanDate.ToLocalTime().ToString(), macAddr);

            //Port Connector
            group = "Win32_PortConnector";
            propArray = new string[] { "InternalReferenceDesignator", "ExternalReferenceDesignator", "PortType" };
            SaveAllDeviceInfo(Environment.MachineName, group, "Port Connector", propArray, scanDate, macAddr);
            htm += "</table>";
         return htm;
        }
 
        public string SaveAllDeviceInfo(string machine, string group, string dgroup, string[] property, string sDate, string mcAddress)
        {
            var value = string.Empty;
            var content = string.Empty;
            try
            {
                var i = 0;
                var ManagementClass1 = new ManagementClass(group);
                var ManagemenobjCol = ManagementClass1.GetInstances();
                var properties = ManagementClass1.Properties;

                foreach (var obj in ManagemenobjCol)
                {
                    foreach (var prop in properties)
                    {
                        try
                        {
                            for (int x = 0; x < property.Length; x++)
                            {
                                value = obj.Properties[property[x]].Value.ToString();
                                content += @"<tr><td><input style='border:none;' name='machinename" + inputIndex + "' value='" + Environment.MachineName + "'/></td><td><input style='border:none;' name='group" + inputIndex + "' value='" + group + "'/></td><td><input style='border:none;' name='dgroup" + inputIndex + "' value='" + dgroup + "'/></td><td><input style='border:none;' name='sequence" + inputIndex + "' value='" + ++i + "'/></td><td><input style='border:none;' name='property" + inputIndex + "' value='" + property[x] + "'/></td><td><input style='border:none;' name='value" + inputIndex + "' value='" + value
                                    + "'/></td><td><input style='border:none;' name='scanDate" + inputIndex + "' value='" + sDate + "'/></td><td><input style='border:none;' name='macAddress" + inputIndex + "' value='" + mcAddress + "'/></td></tr>";
                                inputIndex++;
                            }
                            i = 0;
                            break;
                        }
                        catch
                        {
                        }
                    }

                }

            }
            catch
            {
            }
            return content;
        }
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }

        
    }
}
