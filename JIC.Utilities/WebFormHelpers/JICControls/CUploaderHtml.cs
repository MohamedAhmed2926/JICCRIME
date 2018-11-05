using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using JIC.Base;
using System.Web.Script.Serialization;

namespace JIC.Utilities.WebFormHelpers.JICControls
{
    public class CUploaderHtml : WebControl
    {
        #region Variables

        Table fileUpld;
        CCustomValidator cstmValidator;
        CHiddenField hfUpldFiles;
        CHiddenField hfClear;
        UpdatePanel up;
        HtmlGenericControl uploaderError;
        CLabel lbl;
        HtmlGenericControl container;
        Image pickFile;
        Button btnUploadFinished;

        #endregion

        public event EventHandler UploadCompleted;

        #region Properties

        /// <summary>
        /// Gets or sets the resource key will be used to get the associated label text.
        /// </summary>
        public string LabelResourceKey
        {
            get { return ViewState.Get("LabelResourceKey"); }
            set { ViewState.Set<string>("LabelResourceKey", value); }
        }

        /// <summary>
        /// Gets or sets the accepted file type.
        /// Default is FileTypes.AnyType.
        /// </summary>
        public FileTypes FilesType
        {
            get { return ViewState.Get<FileTypes>("FilesType", FileTypes.AnyType); }
            set { ViewState.Set<FileTypes>("FilesType", value); }
        }

        /// <summary>
        /// Gets or sets the name of the js function to be called after the upload is success.
        /// Default is stirng.empty.
        /// </summary>
        public string OnUploadSuccessClientFunction
        {
            get
            {
                string fn = ViewState.Get<string>("OnUploadSuccessClientFunction", string.Empty);
                if (!string.IsNullOrEmpty(fn))
                    fn = fn + "(fileUpld,UploadedFiles);";
                return fn;
            }
            set { ViewState.Set("OnUploadSuccessClientFunction", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether multiple files can be uploaded.
        /// Default is false.
        /// </summary>
        public bool AllowMultiple
        {
            get { return ViewState.Get<bool>("AllowMultiple", false); }
            set { ViewState.Set<bool>("AllowMultiple", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether the control will show or show the completed ui after upload complete.
        /// Default is true.
        /// </summary>
        public bool ShowCompletedUIAfterComplete
        {
            get { return ViewState.Get<bool>("ShowCompletedUIAfterComplete", false); }
            set { ViewState.Set<bool>("ShowCompletedUIAfterComplete", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the control is required to have value or not.
        /// The default is false.
        /// </summary>
        public bool IsRequired
        {
            get { return ViewState.Get<bool>("IsRequired", false); }
            set { ViewState.Set<bool>("IsRequired", value); }
        }

        /// <summary>
        /// Gets or sets the resource key will be used to get the required validator error message.
        /// Default is "RequiredErrorMessage".
        /// </summary>
        public string RequiredValidatorErrorMessageResourceKey
        {
            get { return ViewState.Get<string>("RequiredValidatorErrorMessageResourceKey", "RequiredErrorMessage"); }
            set { ViewState.Set<string>("RequiredValidatorErrorMessageResourceKey", value); }
        }

        /// <summary>
        /// Gets or sets the name of the validation group to which the uploader required validator belongs.
        /// Default is string.Empty
        /// </summary>
        public string ValidationGroup
        {
            get { return ViewState.Get<string>("ValidationGroup", ""); }
            set { ViewState.Set<string>("ValidationGroup", value); }
        }

        /// <summary>
        /// Gets the list contains the current uploaded files
        /// </summary>
        public List<UploadedFile> UploadedFiles
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(hfUpldFiles.Value))
                        return new List<UploadedFile>();

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    return js.Deserialize<List<UploadedFile>>(hfUpldFiles.Value);
                }
                catch
                {
                    return new List<UploadedFile>();
                }
            }
        }

        /// <summary>
        /// Gets or sets the max count of the uploaded files.
        /// Default is SystemConfigurations.Defaults_MaxFilesUploadCount.
        /// </summary>
        /// <remarks>
        /// If 0, no limit will be used.
        /// </remarks>
        public int MaxFilesCount
        {
            get { return (ViewState.Get<int>("MaxFilesCount", SystemConfigurations.Defaults_MaxFilesUploadCount)); }
            set { ViewState.Set<int>("MaxFilesCount", value); }
        }

        /// <summary>
        /// Gets or sets the max file size in mb for the uploaded files.
        /// Default is SystemConfigurations.Defaults_MaxFileSize.
        /// </summary>
        /// <remarks>
        /// If 0, no size limit will be used.
        /// </remarks>
        public long MaxFileSize
        {
            get { return (ViewState.Get<long>("MaxFileSize", SystemConfigurations.Defaults_MaxFileSize)); }
            set { ViewState.Set<long>("MaxFileSize", value); }
        }

        /// <summary>
        /// Gets or sets the value indecates if script was registered or not.
        /// </summary>
        private bool ScriptRegistered
        {
            get
            {
                // When doing partial postbacks, we need to register the scripts once.

                var scriptManager = ScriptManager.GetCurrent(this.Page);
                if (scriptManager != null && scriptManager.IsInAsyncPostBack)
                    return (ViewState.Get<bool>("IsScriptRegister", false));

                // When doing full postbacks, we register every time.
                return false;
            }
            set
            {
                ViewState.Set<bool>("IsScriptRegister", value);
            }
        }

        private string UploaderHandlerUrl { get { return "/Handlers/UploaderHandler.ashx"; } }

        private string ShowErrorFunction { get { return "function (message) {debugger;$('#_uploadError').text(message);$('#_uploadError').show();}".Replace("_uploadError", uploaderError.ClientID); } }

        private string UploadedFilesName
        {
            get
            {
                return "UploadedFilesName_" + this.ClientID;
            }
        }

        private string ClientValidationFunctionName
        {
            get
            {
                return "chkupld_" + this.ClientID;
            }
        }

        #endregion

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            btnUploadFinished = new Button { ID = this.ClientID + "_btnUploadFinished", CausesValidation = false };
            btnUploadFinished.Style.Add("display", "none");
            btnUploadFinished.Click += (s, ea) =>
            {
                if (UploadCompleted != null)
                    UploadCompleted(s, ea);
            };
            ScriptManager.GetCurrent(Page).RegisterAsyncPostBackControl(btnUploadFinished);
        }

        protected override void OnLoad(EventArgs e)
        {
            lbl = Utilities.CreateLabel(this.LabelResourceKey);
            lbl.Style.Add("vertical-align", "top");
            fileUpld = new Table()
            {
                ID = "fileUpld_" + this.ClientID,
                BorderWidth = 1,
                CssClass = "table table-bordered"
            };
            fileUpld.Style.Add("display", "none");
            TableHeaderRow tr = new TableHeaderRow();
            tr.Cells.Add(new TableHeaderCell()
            {
                Text = "اسم الملف"
            });
            tr.Cells.Add(new TableHeaderCell()
            {
                Text = "نسبة الرفع"
            });
            tr.Cells.Add(new TableHeaderCell()
            {
                Text = "مسح"
            });
            fileUpld.Rows.Add(tr);
            container = new HtmlGenericControl("div")
            {
                ID = "container" + this.ClientID
            };


            container.Controls.Add(btnUploadFinished);

            pickFile = new Image
            {
                ID = this.ClientID + "pickFile",
                ImageUrl = "/Layouts/img/uploadify-upload.png",
                AlternateText = "[Select Files]"
            };
            container.Controls.Add(pickFile);


            if (this.IsRequired)
            {
                cstmValidator = new CCustomValidator
                {
                    ID = "cstmValidator_" + this.ClientID,
                    ErrorMessageResourceKey = this.RequiredValidatorErrorMessageResourceKey,
                    ValidationGroup = this.ValidationGroup,
                    ClientValidationFunction = this.ClientValidationFunctionName
                };

                cstmValidator.Style.Add("position", "relative");
                cstmValidator.Style.Add("top", "-65%");
            }

            hfUpldFiles = new CHiddenField { ID = "hfUpldFiles_" + this.ClientID };
            hfClear = new CHiddenField { ID = "hfClear_" + this.ClientID };
            up = new UpdatePanel { ID = "up_" + this.ClientID };

            uploaderError = new HtmlGenericControl("p") { ID = "uploaderError_" + this.ClientID };
            uploaderError.Attributes.Add("class", "alert alert-error");
            uploaderError.Attributes.Add("style", "display:none;width:50%;");

            up.ContentTemplateContainer.Controls.Add(lbl);

            if (this.IsRequired)
            {
                up.ContentTemplateContainer.Controls.Add(new Label { CssClass = "required", Text = "*" });
                up.ContentTemplateContainer.Controls.Add(cstmValidator);
            }

            up.ContentTemplateContainer.Controls.Add(container);
            up.ContentTemplateContainer.Controls.Add(fileUpld);


            up.ContentTemplateContainer.Controls.Add(hfUpldFiles);
            up.ContentTemplateContainer.Controls.Add(hfClear);
            up.ContentTemplateContainer.Controls.Add(uploaderError);
            Controls.Add(up);

            JIC.Crime.View.WebFormHelpers.Utilities.AddCssToPageHeader(this.Page.Header, "/Layouts/css/jquery.plupload.queue.css");
            JIC.Crime.View.WebFormHelpers.Utilities.AddJsToPageHeader(this.Page.Header, "/Layouts/js/plupload/plupload.full.min.js");
            //JIC.View.Utilities.Utilities.AddJsToPageHeader(this.Page.Header, "/Layouts/js/plupload/i18n/{langCode}.js");
        }

        protected override void OnPreRender(EventArgs e)
        {
            string uploadedFiles = "";
            string chkUpld = "";

            if (!this.ScriptRegistered)
            {
                uploadedFiles = string.Format(";var {0} =[];", UploadedFilesName);
                chkUpld = (@";function chkUpld(sender, args) {
                                args.IsValid = UploadedFiles.length > 0;
                                };")
                                   .Replace("chkUpld", this.ClientValidationFunctionName)
                                   .Replace("UploadedFiles", this.UploadedFilesName);

                this.ScriptRegistered = true;
            }

            bool clear = string.IsNullOrEmpty(hfClear.Value) ? false : bool.Parse(hfClear.Value);

            if (clear)
            {
                uploadedFiles = string.Format(";{0} =[];", UploadedFilesName);
                hfClear.Value = "false";
            }

            string script = (@"
var UploadedFiles = [];
// Initialize the widget when the DOM is ready
$(function() {
    // Setup html5 version
    var uploader = new plupload.Uploader({
        // General settings
        runtimes : 'html5,flash,silverlight,html4',
        browse_button : '_pickfiles', // you can pass in id...
        container: document.getElementById('_container'), // ... or DOM Element itself
        url : 'UploaderHandler',
        file_data_name : 'Filedata',
        chunk_size : '1mb',
        rename : true,
        dragdrop: true,
        max_file_size : '_fileSizeLimit',

        filters : {
            // Maximum file size
            max_file_size : '10mb',
            // Specify what files to browse for
            mime_types: [
                {title : ""Image files"", extensions : ""_fileTypeExts""}
            ]
        },
          
        // Flash settings
        flash_swf_url : '/Layouts/js/plupload/js/Moxie.swf',
     
        // Silverlight settings
        silverlight_xap_url : '/Layouts/js/plupload/js/Moxie.xap',
        init: {
            PostInit: function() {
                UploadedFiles = [];
            },
 
            FilesAdded: function(up, files) {
                plupload.each(files, function(file) {
                    var table = document.getElementById(""_fileUpld"");
                    var row = table.insertRow(table.rows.length);
                    row.id = 'upload_'+file.id;
                    var cell1 = row.insertCell(0);
                    var cell2 = row.insertCell(1);
                    row.insertCell(2);
                    // Add some text to the new cells:
                    cell1.innerHTML =file.name+ ' (' + plupload.formatSize(file.size)+')';
                    $(""#_fileUpld"").show();
                });
                uploader.start();
            },
 
            UploadProgress: function(up, file) {
                var row = document.getElementById('upload_'+file.id);
                row.cells[1].innerHTML =file.percent+'%';
                if(file.percent == 100){
                    row.cells[2].innerHTML = '<img class=""removeBtn"" style=""display:none"" src=""_cancelImg"" onclick=""removeFile(&#39;'+file.id+'&#39;)""/> ';
                }
            },
          
			FileUploaded:function(up,file,response){
				if(response.response.indexOf(""error"") == 0){
					console.log(response.response.split(""|"")[1]);
					file.status=plupload.FAILED;
					up.trigger('UploadProgress',file);
				}else{
					var _UploadedFiles = [];
					for (var i = 0; i < UploadedFiles.length; i++) {
						if (file.name != UploadedFiles[i].ClientName)
							_UploadedFiles.push(UploadedFiles[i]);
					}
					UploadedFiles = _UploadedFiles;

					var paths=response.response.split(';');
					UploadedFiles.push({
						FileID: file.id,
						FileName: file.name,
						Index: file.index,
						Type: file.type,
						PhysicalPath: paths[0],
						VirtualPath: paths[1]
					});
					_onUploadSuccess;
				}
			}, 
            UploadComplete : function(up,files){
                $('#hfUpldFiles').val(JSON.stringify(UploadedFiles));
                var _maxFileCnt = _maxAllowedNofFiles;
                if(_maxFileCnt == 0)
                    _maxFileCnt = 10000;

                if(!_allowMultiple)
                    _maxFileCnt = 1;

                //if(UploadedFiles.length >= _maxFileCnt)
                //{
                 //       $('#fileUpld').uploadify('settings', 'height', 0);
                 //       $('#fileUpld').uploadify('disable', true);
                //}
                var _removeBtn = document.getElementsByClassName('removeBtn');
                for (var i = 0; i < _removeBtn.length; ++i) {
                    var item = _removeBtn[i];  
                    item.style.display = '';
                }
				$('#_btnUploadFinished').click();
            },
 
            Error: function(up, err) {
                console.log('error: ' + err.response);
            }
        }
     });
    uploader.init();
});
    function ClearUploader(){
        $('#hfUpldFiles').val('');
        UploadedFiles =[];
    }

    function removeFile(id){
            var _UploadedFiles = [];
            for (var i = 0; i < UploadedFiles.length; i++) {
                if (id != UploadedFiles[i].FileID)
                    _UploadedFiles.push(UploadedFiles[i]);
            }
            UploadedFiles = _UploadedFiles;
        removeRow(id);
        $('#hfUpldFiles').val(JSON.stringify(UploadedFiles)); 
    }
    function removeRow(id){
        var row = document.getElementById('upload_'+id);
        if(row != null)
            row.parentNode.removeChild(row);


        var table = document.getElementById(""_fileUpld"");
        if(table.rows.length == 1){
            $(""#_fileUpld"").hide();
        }
        
    }
    ;");

            string userAgent = HttpContext.Current.Request.UserAgent.ToLower();
            string flashDownloadUrl = userAgent.Contains("android") ? "http://fpdownload.macromedia.com/pub/flashplayer/installers/archive/android/11.1.115.81/install_flash_player_ics.apk" : "http://www.adobe.com/go/getflashplayer";
            script = script.Replace("_onUploadSuccess", OnUploadSuccessClientFunction)
                .Replace("_allowMultiple", AllowMultiple.ToString().ToLower())
                .Replace("_fileUpld", fileUpld.ClientID)
                .Replace("_container", container.ClientID)
                .Replace("_pickfiles", pickFile.ClientID)
                .Replace("_fileSizeLimit", MaxFileSize.ToString() + "mb")
                .Replace("_fileTypeExts", GetFilesTypes(FilesType))
                .Replace("UploaderHandler", UploaderHandlerUrl)
                .Replace("UploadedFiles", UploadedFilesName)
                .Replace("_maxAllowedNofFiles", MaxFilesCount.ToString())
                .Replace("hfUpldFiles", hfUpldFiles.ClientID)
                .Replace("hfClear", hfClear.ClientID)
                .Replace("_cancelImg", "/Layouts/img/uploadify-cancel.png")
                .Replace("_btnUploadFinished", btnUploadFinished.ClientID);
            //.Replace("_uploadLimit", "0");
            //.Replace("cstmValidator", cstmValidator.ClientID)
            //.Replace("_multi", AllowMultiple.ToString().ToLower())
            //.Replace("_showError", ShowErrorFunction)
            //.Replace("_removeCompleted", (!ShowCompletedUIAfterComplete).ToString().ToLower())
            //.Replace("defaultFlashImage", string.Concat("\"<a href='", flashDownloadUrl, "' target='_blank'><img src='http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif'  /></a>\""));

            ScriptManager.RegisterStartupScript(up, up.GetType(), this.ClientID + "_script", uploadedFiles + chkUpld + script, true);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            up.RenderControl(writer);
        }

        public void Clear()
        {
            hfUpldFiles.Value = "";
            hfClear.Value = "true";
        }

        string GetFilesTypes(FileTypes FileType)
        {
            string fileTypeExts = string.Empty;

            if (FileType.HasFlag(FileTypes.Excel))
                fileTypeExts += "xls,xlsx,";
            if (FileType.HasFlag(FileTypes.OldExcel))
                fileTypeExts += "xls,";

            if (FileType.HasFlag(FileTypes.PowerPoint))
                fileTypeExts += "ppt,pptx,";

            if (FileType.HasFlag(FileTypes.Word))
                fileTypeExts += "doc,docx,";

            if (FileType.HasFlag(FileTypes.Pdf))
                fileTypeExts += "pdf,";

            if (FileType.HasFlag(FileTypes.Text))
                fileTypeExts += "txt,";

            if (FileType.HasFlag(FileTypes.Images))
                fileTypeExts += "jpg,gif,png,";

            if (FileType.HasFlag(FileTypes.Sound))
                fileTypeExts += "mp3,mp4,";

            if (FileType.HasFlag(FileTypes.Video))
                fileTypeExts += "flv,";

            if (FileType.HasFlag(FileTypes.Winrar))
                fileTypeExts += "rar,";

            if (FileType.HasFlag(FileTypes.Zip))
                fileTypeExts += "zip,";

            if (FileType.HasFlag(FileTypes.AnyType))
                fileTypeExts += "*";

            if (FileType.HasFlag(FileTypes.CSV))
                fileTypeExts += "csv";

            return fileTypeExts;
        }

        #endregion

        #region SubClass

        public class UploadedFile
        {
            public string FileID { get; set; }
            public string FileName { get; set; }
            public int Index { get; set; }
            public string Type { get; set; }
            public string PhysicalPath { get; set; }
            public string VirtualPath { get; set; }
        }

        #endregion
    }
}
