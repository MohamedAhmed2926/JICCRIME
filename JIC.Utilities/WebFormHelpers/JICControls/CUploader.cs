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
    public class CUploader : WebControl
    {
        #region Variables

        FileUpload fileUpld;
        CCustomValidator cstmValidator;
        CHiddenField hfUpldFiles;
        CHiddenField hfClear;
        UpdatePanel up;
        HtmlGenericControl uploaderError;
        CLabel lbl;

        #endregion

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
            get { return (ViewState.Get<long>("MaxFileSize", SystemConfigurations.Defaults_MaxFileSize) * 1024); }
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

        protected override void OnLoad(EventArgs e)
        {
            lbl = Utilities.CreateLabel(this.LabelResourceKey);
            lbl.Style.Add("vertical-align", "top");

            fileUpld = new FileUpload
            {
                ID = "fileUpld_" + this.ClientID,
                AllowMultiple = this.AllowMultiple
            };

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

            up.ContentTemplateContainer.Controls.Add(fileUpld);


            up.ContentTemplateContainer.Controls.Add(hfUpldFiles);
            up.ContentTemplateContainer.Controls.Add(hfClear);
            up.ContentTemplateContainer.Controls.Add(uploaderError);
            Controls.Add(up);

            JIC.Crime.View.WebFormHelpers.Utilities.AddCssToPageHeader(this.Page.Header, "/Layouts/css/uploadify_{langType}.css");
            JIC.Crime.View.WebFormHelpers.Utilities.AddJsToPageHeader(this.Page.Header, "/Layouts/js/uploadify/{langCode}.js");
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
;var hasFlash = false;
try {
    hasFlash = Boolean(new ActiveXObject('ShockwaveFlash.ShockwaveFlash'));
} catch(exception) {
    hasFlash = ('undefined' != typeof navigator.mimeTypes['application/x-shockwave-flash']);
}
if(hasFlash){
    function ClearUploader(){
    $('#hfUpldFiles').val('');
    UploadedFiles =[];
    }

    ;$('#fileUpld').uploadify({
                buttonClass: 'upldbtn',
                buttonImage: '/Layouts/img/uploadify-upload.png',
                buttonText: '',
                width: 50,
                height: 35,
                multi:_multi,
                removeCompleted:_removeCompleted,
                'fileTypeExts' : '_fileTypeExts',
                'fileTypeDesc' : 'Upload Files', 
                showError: _showError,
                fileSizeLimit:_fileSizeLimit,
                uploadLimit:_uploadLimit,
                'swf': '/Layouts/swf/uploadify.swf',
                'uploader': 'UploaderHandler',
                onUploadStart: function (file) {
                    $('#cstmValidator').css({ visibility: 'hidden' });
                },
                onUploadComplete: function (file) {
                 $('#hfUpldFiles').val(JSON.stringify(UploadedFiles));

                    var _maxFileCnt = _maxAllowedNofFiles;
                    if(_maxFileCnt == 0)
                        _maxFileCnt = 10000;

                    if(!_allowMultiple)
                        _maxFileCnt = 1;

                    if(UploadedFiles.length >= _maxFileCnt)
                    {
                          $('#fileUpld').uploadify('settings', 'height', 0);
                          $('#fileUpld').uploadify('disable', true);
                    }

                },
                onUploadSuccess: function (file,data,response) {

                    var _UploadedFiles = [];
                    for (var i = 0; i < UploadedFiles.length; i++) {
                        if (file.name != UploadedFiles[i].ClientName)
                            _UploadedFiles.push(UploadedFiles[i]);
                    }
                    UploadedFiles = _UploadedFiles;

                    var paths=data.split(';');
                    UploadedFiles.push({
                        FileID: file.id,
                        FileName: file.name,
                        Index: file.index,
                        Type: file.type,
                        PhysicalPath: paths[0],
                        VirtualPath: paths[1]
                    });
                    _onUploadSuccess;
                },
                onCancel: function (fileID) {

                    var _maxFileCnt = _maxAllowedNofFiles;
                    if(_maxFileCnt == 0)
                        _maxFileCnt = 10000;

                    if(!_allowMultiple)
                        _maxFileCnt = 1;

                    var _UploadedFiles = [];
                    for (var i = 0; i < UploadedFiles.length; i++) {
                        if (fileID != UploadedFiles[i].FileID)
                            _UploadedFiles.push(UploadedFiles[i]);
                    }
                    UploadedFiles = _UploadedFiles;
                    $('#hfUpldFiles').val(JSON.stringify(UploadedFiles));

                    if(UploadedFiles.length < _maxFileCnt)
                    {
                        $('#fileUpld').uploadify('settings', 'height', 35);
                        $('#fileUpld').uploadify('disable', false);
                    }

                },
            });
}
else
{
    $('#fileUpld').parent().append(defaultFlashImage)
    $('#fileUpld').remove();
}");

            string userAgent = HttpContext.Current.Request.UserAgent.ToLower();
            string flashDownloadUrl = userAgent.Contains("android") ? "http://fpdownload.macromedia.com/pub/flashplayer/installers/archive/android/11.1.115.81/install_flash_player_ics.apk" : "http://www.adobe.com/go/getflashplayer";
            script = script.Replace("_onUploadSuccess", OnUploadSuccessClientFunction)
                .Replace("_allowMultiple", AllowMultiple.ToString().ToLower())
                .Replace("fileUpld", fileUpld.ClientID)
                .Replace("hfUpldFiles", hfUpldFiles.ClientID)
                .Replace("hfClear", hfClear.ClientID)
                .Replace("cstmValidator", cstmValidator.ClientID)
                .Replace("UploadedFiles", UploadedFilesName)
                .Replace("UploaderHandler", UploaderHandlerUrl)
                .Replace("_multi", AllowMultiple.ToString().ToLower())
                .Replace("_showError", ShowErrorFunction)
                .Replace("_fileSizeLimit", MaxFileSize.ToString())
                .Replace("_fileTypeExts", GetFilesTypes(FilesType))
                .Replace("_maxAllowedNofFiles", MaxFilesCount.ToString())
                .Replace("_uploadLimit", "0")
                .Replace("_removeCompleted", (!ShowCompletedUIAfterComplete).ToString().ToLower())
                .Replace("defaultFlashImage", string.Concat("\"<a href='", flashDownloadUrl, "' target='_blank'><img src='http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif'  /></a>\""));

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
                fileTypeExts += "*.xls;*.xlsx;";
            if (FileType.HasFlag(FileTypes.OldExcel))
                fileTypeExts += "*.xls;";

            if (FileType.HasFlag(FileTypes.PowerPoint))
                fileTypeExts += "*.ppt;*.pptx;";

            if (FileType.HasFlag(FileTypes.Word))
                fileTypeExts += "*.doc;*.docx;";

            if (FileType.HasFlag(FileTypes.Pdf))
                fileTypeExts += "*.pdf;";

            if (FileType.HasFlag(FileTypes.Text))
                fileTypeExts += "*.txt;";

            if (FileType.HasFlag(FileTypes.Images))
                fileTypeExts += "*.bmp;*.jpg;*.png;*.gif;";

            if (FileType.HasFlag(FileTypes.Sound))
                fileTypeExts += "*.mp3;*.mp4;";

            if (FileType.HasFlag(FileTypes.Video))
                fileTypeExts += "*.flv;";

            if (FileType.HasFlag(FileTypes.Winrar))
                fileTypeExts += "*.rar;";

            if (FileType.HasFlag(FileTypes.Zip))
                fileTypeExts += "*.zip;";

            if (FileType.HasFlag(FileTypes.AnyType))
                fileTypeExts += "*.*";

            if (FileType.HasFlag(FileTypes.AnyType))
                fileTypeExts += "*.*";

            if (FileType.HasFlag(FileTypes.CSV))
                fileTypeExts += "*.csv";

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
