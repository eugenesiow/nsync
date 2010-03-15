using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Management;

namespace nsync
{
    class Intelligence
    {
        public bool IsFolderExists(string folderPath)
        {
            if (Directory.Exists(folderPath))
                return true;
            else
                return false;
        }

        public bool IsFoldersSimilar(string leftFolderPath, string rightFolderPath)
        {
            if (leftFolderPath == rightFolderPath)
                return true;
            return false;
        }

        public bool IsFolderSubFolder(string leftFolderPath, string rightFolderPath)
        {
            DirectoryInfo leftPathDir = new DirectoryInfo(leftFolderPath);
            DirectoryInfo rightPathDir = new DirectoryInfo(rightFolderPath);
            string leftPathDirParent;
            string rightPathDirParent;

            if (leftPathDir.Parent != null)
                leftPathDirParent = leftPathDir.Parent.FullName.ToString();
            else
                leftPathDirParent = leftPathDir.FullName.ToString();

            if (rightPathDir.Parent != null)
                rightPathDirParent = rightPathDir.Parent.FullName.ToString();
            else
                rightPathDirParent = rightPathDir.FullName.ToString();

            if (leftPathDirParent.Contains(rightFolderPath) || rightPathDirParent.Contains(leftFolderPath))
            {
                return true;
            }
            else
                return false;
        }

        //Checks if drive is removable
        public bool IsRemovableDrive(string path)
        {
            ManagementObjectSearcher mosDisks = new ManagementObjectSearcher(@"SELECT * FROM Win32_LogicalDisk WHERE DriveType=2"); //Finds all removable drives

            foreach (ManagementObject moDisk in mosDisks.Get())
            {
                if (moDisk["DeviceID"].ToString() == Directory.GetDirectoryRoot(path).Remove(2))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
