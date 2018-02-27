<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View_UploadAssets.ascx.cs" Inherits="Hydros.View_UploadAssets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

<script>
    $(document).ready(function () {
        $.fn.UploadFile = function (ItemID, GroupID, Files) {
            var data = new FormData();
            //var files = $('#fileUpload').prop('files');
            var files = Files;
            // Add the uploaded image content to the form data collection
            if (files.length > 0) {
                for (var i = 0; i < files.length; i++) {
                    data.append('UploadedImage' + i, files[i]);
                }

                data.append('ItemID', ItemID);
                data.append('GroupID', GroupID);

                //Make Ajax request with the contentType = false, and procesDate = false
                $.ajax({
                    type: 'POST',
                    url: '/DesktopModules/Hydros/Api/Asset/UploadFile',
                    data: data,
                    //contentType: "multipart/form-data",
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data = 201) {
                            alert('Upload Success!');
                        }
                        else {
                            alert("Upload Fail!");
                        }
                    },
                    error: function (e) {
                        alert(e);
                    }
                });
            }
        }

        $('#btnUploadFile').click(function () {
            $.fn.UploadFile('B1398-36', 'B1398', $('#fileUpload').prop('files'));
        });
    });
</script>
<div>
    <label for="fileUpload" />
    Select File to Upload: <input id="fileUpload" type="file" multiple="multiple" />

    <input id="btnUploadFile" type="button" value="Upload File" />
</div>



<script>
    $(document).ready(function () {
        $("#<%=sltAccessType.ClientID%>").change(function () {
            var val = $(this).val();
            if (val == "Photo") {
                $("#<%=sltItemType.ClientID%>").html("<option value='Item'>Item Photo</option><option value='Group'>Group Photo</option>");
            } else if (val == "Document") {
                $("#<%=sltItemType.ClientID%>").html("<option value='CustomerCareBulletins'>Customer Care Bulletins</option>" +
                                                     "<option value='HardwareLocation'>Hardware Location</option>" +
                                                     "<option value='ProductDimensions'>Product Dimensions</option>" +
                                                     "<option value='ServicePartsList'>Service Parts List</option>" +
                                                     "<option value='StudySheets' selected='selected'>Study Sheets</option>" +
                                                     "<option value='AssemblyInstructions'>Assembly Instructions</option>" +
                                                     "<option value='ProductLiterature'>Product Literature</option>");
            } else if (val == "Video") {
                $("#<%=sltItemType.ClientID%>").html("<option value='Item'>Item Photo</option><option value='Group'>Group Photo</option>");
            }
        });
        //Copy and remove space Group and Item
        $("#<%=txtGroupID.ClientID%>").keyup(function () {
            var val = $(this).val().replace(/ /g, '');
            $("#<%=txtGroupID.ClientID%>").val(val);
            $("#<%=txtItemID.ClientID%>").val(val);
        });
    });
</script>
<div class="row">
  <div class="col-sm-6">
      <div class="demo-container size-narrow">
        <div id="DropZone">
            <p>Drop Files Here</p>
        </div>
        <telerik:RadAsyncUpload ID="radAUpload" runat="server" MultipleFileSelection="Automatic" DropZones="#DropZone" RenderMode="Lightweight" EnableInlineProgress="false" ></telerik:RadAsyncUpload>
    </div>
  </div>
  <div class="col-sm-6">
    <form data-toggle="validator" role="form" id="upload">
        <div class="form-group">
            <label for="txtGroupID">GroupID:</label>
            <asp:TextBox ID="txtGroupID" runat="server" class="form-control" placeholder="Enter GroupID" required></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtItemID">ItemID:</label>
            <asp:TextBox ID="txtItemID" runat="server" class="form-control" placeholder="Enter ItemID" required></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="sltAccessType">Select Asset Type:</label>
            <select class="form-control" id="sltAccessType" runat="server">
                <option value="Photo">Photo</option>
                <option value="Document">Document</option>
                <option value="Video">Video</option>
            </select>
        </div>
        <div class="form-group">
            <label for="sltItemType">Select Item Type:</label>
            <select class="form-control" id="sltItemType" runat="server">
                <option value="Item">Item Photo</option>
                <option value="Group">Group Photo</option>
            </select>
        </div>
    
        <asp:Button ID="btUpload" runat="server" Text="Upload" OnClick="btUpload_Click" class="btn-primary btn-lg btn-block" />
    </form>
  </div>
</div>

