using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebVault
{
    public partial class Browser : System.Web.UI.Page
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
           
            if (!IsPostBack)
            {
                Session["CurrentView"] = "icon";
                Session["Files"] = GetFiles();
            }
            
            BuildTree(UserPath);
            UpdateView("icon", Session["Files"] as BindingFileList);
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
                var name = file.Name;
                if (file.Name.Contains("_-_"))
                {
                    name = name.Split(new string[] { "_-_" }, StringSplitOptions.None)[1];
                }
                var sub = new TreeNode(name);
                
                var enc = Convert.ToBase64String(Encoding.UTF8.GetBytes(file.FullName));

                sub.NavigateUrl = "javascript:fileNodeClick('" + enc + "');";
                directoryNode.ChildNodes.Add(sub);
            }
            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(directoryInfo.FullName));
            directoryNode.NavigateUrl = "javascript:dirNodeClick('" + encoded+ "');";
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

    

        protected void DirTree_SelectedNodeChanged(object sender, EventArgs e)
        {

        }

        protected void DirTree_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

        }

        protected void Unnamed_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void Unnamed_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        protected void UpdateView(string view, object datasource = null)
        {
            if (datasource == null)
            { datasource = GetFiles().OrderBy(w => w.Title); }
            switch (view)
            {
                case "icon":
                    FileRepeater.ItemTemplate = Page.LoadTemplate("/Templates/icon_view.ascx");
                    FileRepeater.DataSource = datasource;
                    FileRepeater.DataBind();
                    Session["CurrentView"] = "icon";
                    break;
                case "detail":

                    break;
                case "list":
                    FileRepeater.ItemTemplate = Page.LoadTemplate("/Templates/icon_view.ascx");
                    FileRepeater.DataSource = datasource;
                    FileRepeater.DataBind();
                    Session["CurrentView"] = avSelectedView.Text;
                    break;               
                default:
                    FileRepeater.ItemTemplate = Page.LoadTemplate("/Templates/icon_view.ascx");
                    FileRepeater.DataSource = datasource;
                    FileRepeater.DataBind();
                    Session["CurrentView"] = avSelectedView.Text;
                    break;
            }
        }

        private BindingFileList GetFiles(string relativePath=null)
        {
            var userid = ECDHAES256s.AES.Sha256(User.Identity.Name);
            if (relativePath==null)
            {
                UserPath = "/Upload/Files/" + userid + "/";
            }
            else
            {
                UserPath ="/Upload"+ relativePath;
            }
            BrowserMulti.ActiveViewIndex = 0;           
            
            //get files
            List<FileFolderInfo> filesAndFolders = new List<FileFolderInfo>();
            var directories = Directory.GetDirectories(Server.MapPath(UserPath));
            foreach (var dir in directories)
            {
                var tmp = dir;
                var split = tmp.Split(new string[] { "\\" }, StringSplitOptions.None).ToList();
                var name = split.Last();
                filesAndFolders.Add(new FileFolderInfo(new DirectoryInfo(dir)));
            }
            var files = Directory.GetFiles(Server.MapPath(UserPath));
            foreach (var file in files)
            {
                var a = new System.IO.FileInfo(file);
                filesAndFolders.Add(new FileFolderInfo(a));
            }
            BindingFileList ds = new BindingFileList();
            filesAndFolders.ForEach(i => { ds.Add(i); });
            Session["UserPath"] = UserPath;
            return ds;
        }

        protected void avChangeView_Click(object sender, EventArgs e)
        {

        }

        protected void DirClick_Click(object sender, EventArgs e)
        {
            var userid = ECDHAES256s.AES.Sha256(User.Identity.Name);
            UserPath = "/Upload/Files/" + userid + "/";
            BrowserMulti.ActiveViewIndex = 0;
            var dirId = DirID.Text;
            try
            {
                dirId = Encoding.UTF8.GetString(Convert.FromBase64String(dirId));

            }
            catch
            {
                dirId = DirID.Text;
            }
            if (dirId ==userid || dirId==User.Identity.Name )
            {
                UserPath = "/Upload/Files/" + userid + "/";
            }else
            {
                //find actual path
                try
                {
                    var split = dirId.Split(new string[] { "\\Upload"}, StringSplitOptions.None);
                    var tmp = split.Last().Replace("\\", "/");
                    var bb = 0;
                    UserPath = "/Upload" + tmp;
                }
                catch
                {

                }
            }
            //get files
            List<FileFolderInfo> filesAndFolders = new List<FileFolderInfo>();
            var directories = Directory.GetDirectories(Server.MapPath(UserPath));
            foreach (var dir in directories)
            {
                var tmp = dir;
                var split = tmp.Split(new string[] { "\\" }, StringSplitOptions.None).ToList();
                var name = split.Last();
                filesAndFolders.Add( new FileFolderInfo(new DirectoryInfo(dir)));
            }
            var files = Directory.GetFiles(Server.MapPath(UserPath));
            foreach(var file in files)
            {
                var a = new System.IO.FileInfo(file);
                filesAndFolders.Add(new FileFolderInfo(a));
            }
            BindingFileList ds = new BindingFileList();
            filesAndFolders.ForEach(i => { ds.Add(i); });
            UpdateView("icon", ds);
            Session["UserPath"] = UserPath;
        }

        protected void FileClick_Click(object sender, EventArgs e)
        {
            BrowserMulti.ActiveViewIndex = 1;
            var fileId = FileID.Text;
        }

        protected void FileRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this);
            LinkButton btn = e.Item.Controls[0].FindControl("IconLink") as LinkButton;
            if (btn != null)
            {
               btn.Click += LinkButton1_Click;
                scriptMan.RegisterAsyncPostBackControl(btn);
            }
        }

        private void LinkButton1_Click(object sender, EventArgs e)
        {
           var button = sender as LinkButton;
            var path = Encoding.UTF8.GetString(Convert.FromBase64String( button.CommandName));
            var task = button.CommandName;
            var split = path.Split(new string[] { "\\Upload" }, StringSplitOptions.None);
            path = split[1].Replace("\\", "/");
            UserPath = path;
            Session["UserPath"] = UserPath;
            UpdateView("icon", GetFiles(path));
            FileBoxUpdatePanel.Update();
  
        }

        protected void FileRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        
        }
    }
}