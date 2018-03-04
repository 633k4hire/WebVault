using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebVault
{
    public partial class FileBrowser : System.Web.UI.Page
    {
        public string TreeHtml = "";
        public DirectoryTree _Tree;
        string[] _goodFileTypes = new string[] { ".gif", ".pdf", ".png", ".jpg", ".jpeg", ".doc", ".docx", ".xls", ".xlsx", ".psd", ".eps", ".zip", ".ai", ".ppt", ".pptx" };
        public string UserPath { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            DirTree.Attributes.Add("onclick", "return treeClick(event)");

            var userid = ECDHAES256s.AES.Sha256(User.Identity.Name);
            UserPath = "/Upload/Files/" + userid + "/";
            //_Tree = BuildTree(UserPath);

            BuildTree(UserPath);

        }

        private static string CreateDirectoryHtml(DirectoryInfo directoryInfo, string html = "")
        {

            html += "<li data-icon='< span class='glyphicon glyphicon-folder-close'></span>' data-caption='" + directoryInfo.Name + "'>";

            //var directoryNode = new DirectoryTreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                html = CreateDirectoryHtml(directory, html);
            }
            if (directoryInfo.GetFiles().Length > 0)
            {
                html += "<ul>";
                foreach (var file in directoryInfo.GetFiles())
                {
                    html += "<li data-icon='< span class='glyphicon glyphicon-file'></span>' data-caption='" + file.Name + "'></li>";
                }
                html += "</ul>";
            }

            html += "</li>";
            return html;
        }

        protected void TreeRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var item = e.Item;
            if (item.ItemType == ListItemType.Item)
            {
                var dir = item.DataItem as string;
                //var node = item.DataItem as DirectoryTreeNode;
                var html = CreateDirectoryHtml(new DirectoryInfo(dir));
                var control = item.Controls[0];
                var literal = control as LiteralControl;
                literal.Text = html;
            }


        }

        protected void TreeRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        public bool isRoot = true;
        private void BuildTree(string relativePath)
        {
            TreeNodeCollection tree = null;
            if (Directory.Exists(Server.MapPath(relativePath)))
            {
                DirTree.Nodes.Clear();
                var rootDirectoryInfo = new DirectoryInfo(Server.MapPath(relativePath));
                DirTree.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            }


        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            TreeNode directoryNode = null;
            if (isRoot)
            {
                directoryNode = new TreeNode(User.Identity.Name);
                isRoot = false;
            }
            else
            {
                directoryNode = new TreeNode(directoryInfo.Name);

            }

            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.ChildNodes.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
            {
                //split guid
                if (file.Name.Contains("_-_"))
                {
                    var split = file.Name.Split(new string[1] { "_-_" }, StringSplitOptions.None);
                    var guid = split[0];
                    var name = split[1];
                    var sub = new TreeNode(name);
                    sub.NavigateUrl = "javascript:fileNodeClick('" + guid + "');";
                    directoryNode.ChildNodes.Add(sub);
                }
                else
                {

                    var sub = new TreeNode(file.Name);
                    sub.NavigateUrl = "javascript:fileNodeClick('" + file.Name + "');";
                    directoryNode.ChildNodes.Add(sub);

                }
                
            }

            directoryNode.NavigateUrl = "javascript:dirNodeClick('" + directoryInfo.Name + "');";
            return directoryNode;
        }

        private bool CanViewFiles(DirectoryInfo dir)
        {
            return true;
            var config = WebConfigurationManager.OpenWebConfiguration(Server.MapPath(dir.FullName));
            if (config != null && config.HasFile)
            {
                var section = config.GetSection("system.web/authorization") as AuthorizationSection;
                if (section != null)
                {
                    foreach (AuthorizationRule rule in section.Rules)
                    {
                        if (rule.Action == AuthorizationRuleAction.Allow)
                        {
                            foreach (string role in rule.Roles)
                            {
                                if (HttpContext.Current.User.IsInRole(role))
                                {
                                    return true;
                                }
                            }

                            foreach (string user in rule.Users)
                            {
                                if (HttpContext.Current.User.Identity.Name == user)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }
            }
            return true;
        }

        private string FileLength(long fileSize)
        {
            string startSpan = "<span style=\"font-size:.8em;margin-left: 10px;\">";
            string endSpan = "</span>";
            return fileSize <= 999999 ? String.Format(" {0}{1}{2}{3}", startSpan, (Math.Round((fileSize / 1024f), 2)).ToString(), " KB", endSpan) :
                String.Format(" {0}{1}{2}{3}", startSpan, (Math.Round((fileSize / 1024000f), 2)).ToString(), " MB", endSpan);
        }

        protected void DirTree_SelectedNodeChanged(object sender, EventArgs e)
        {

        }

        protected void DirTree_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

        }
    }

}