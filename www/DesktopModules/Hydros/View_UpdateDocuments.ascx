<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View_UpdateDocuments.ascx.cs" Inherits="Hydros.View_UpdateDocuments" %>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<script src="/DesktopModules/Hydros/JS/alert.js"></script>
<link href="//cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
<script src="//cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>
<script src="/DesktopModules/Hydros/JS/dropzone.js"></script>
<link href="/DesktopModules/Hydros/JS/dropzonecss.css" rel="stylesheet" />

<script>
    $(document).ready(function () {

        var tblDocument = $('#tblDocumentList').DataTable({
            paging: false,
            searching: false,
            ordering: false,
            info: false,
            ajax: {
                url: '/DesktopModules/Hydros/Api/Asset/DocumentGetByItem?ItemID=<%= ItemID %>',
                dataSrc: '',
                async: false
            },
            columns: [
                { data: 'AssetID' },
                { data: 'ItemType' },
                { data: 'IsDeleted' }
            ],
            columnDefs: [
                 {
                     render: function (data, type, row) {
                         return '<a target="blank" href="' + row.URLDoc + '" ><img src="' + getIcon(row.URLDoc) + '" style="height: 50px;" /></a>';
                     },
                     targets: 0
                 },
                 {
                     render: function (data, type, row) {
                         return '<input type="checkbox" name="chbCheck" value="' + row.AssetID + '">';
                     },
                     className: 'dt-center',
                     targets: 2
                 }
            ]
        });

        function getIcon(strfile)
        {
            var ext = strfile.substr((strfile.lastIndexOf('.') + 1));
            if (ext == "pdf")
                return "/DesktopModules/Hydros/Images/pdf.svg";
            if (ext == "doc" || ext == "docx")
                return "/DesktopModules/Hydros/Images/doc.svg";
            if (ext == "xls" || ext == "xlsx")
                return "/DesktopModules/Hydros/Images/xls.svg";
        }


        //DropZone
        $(function () {
            var myDropzone = new Dropzone("#dropzone", {
                url: '/file/post',
                uploadMultiple: false,
                //acceptedFiles: "document/*",
                previewsContainer: false
            });
            //How to input GroupID for B1398 ex
            myDropzone.on("addedfile", function (file) {
                //get ItemType
                var ItemType = $('#sltDocType').val();

                $.fn.UploadFile('<%= ItemID %>', '<%= GroupID %>', ItemType, 'Document', file);
            });
        });

        //Upload Files
        $.fn.UploadFile = function (ItemID, GroupID, ItemType, AssetType, file) {
            
            var data = new FormData();
            //var files = $('#fileUpload').prop('files');
            var files = file;
            // Add the uploaded image content to the form data collection
            data.append('UploadedDocument', file);
            data.append('ItemID', ItemID);
            data.append('GroupID', GroupID);
            data.append('ItemType', ItemType);
            data.append('AssetType', AssetType);
                
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
                        //Reload Table, Re-Assign Check Primay and Radio Change
                        tblDocument.ajax.reload();
                        console.log('ok');
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

        jQuery(function ($) {
            $(document).ajaxStart(function () {
                $("#ajax_loader").show();
                $("#dropzone").css("opacity", 0.2);
            }).ajaxStop(function () {
                $("#ajax_loader").hide();
                $("#dropzone").css("opacity", 1);
            });
        });

        //Delete Document
        $("#btnUpdate").click(function (e) {
            $('input[type=checkbox][name=chbCheck]').each(function () {
                if ($(this).prop('checked') == true) {
                    var AssetID = $(this).val();

                    //Call API to Delete
                    $.ajax({
                        type: 'DELETE',
                        url: '/DesktopModules/Hydros/Api/Asset/Delete?AssetID=' + AssetID,
                        success: function (data) {
                            if (data = 201) {
                                e.preventDefault();
                            }
                            else {
                                alert("Fail!");
                            }
                        }
                    });
                }
            });
            runEffect();
            setTimeout("window.open(self.location, '_self');", 1000);
        });

        //Show alert after update successfully
        function runEffect() {
            $.alert('This alert box could indicate a successful or positive action', {
                title: 'Success!',
                closeTime: 1 * 10000,
                autoClose: 'true',
                position: 'bottom-left',
                withTime: $('#withTime').is(':checked'),
                type: "success",
                isOnly: !$('#isOnly').is(':checked')
            });

        };
    })
</script>

<div id="dropzone" class="DropArea">
    Select Document Type then Drop files here or click to upload. 
    <h4>
        <span class="label label-default"><%= ItemID %></span>
        <span class="label label-default"><%= AssetType %></span>
    </h4>

     <select class="form-control" id="sltDocType">
        <option value='CustomerCareBulletins'>Customer Care Bulletins</option>
        <option value='HardwareLocation'>Hardware Location</option>
        <option value='ProductDimensions'>Product Dimensions</option>
        <option value='ServicePartsList'>Service Parts List</option>
        <option value='StudySheets' selected='selected'>Study Sheets</option>
        <option value='AssemblyInstructions'>Assembly Instructions</option>
        <option value='ProductLiterature'>Product Literature</option>
    </select>

    <table id="tblDocumentList" class="table table-hover">
        <thead>
            <tr>
                <th>Document</th>
                <th>DocType</th>
                <th class="text-center">Check to delete</th>
            </tr>
        </thead>
    </table>
    <br />
    <button id="btnUpdate" class="btn-primary btn-lg btn-block">Update <strong><%= ItemID %></strong></button>
</div>

<!-- Loading Panel -->
<div class="loader centered" id="ajax_loader" style="display:none;"></div>
<!-- End Loading Panel -->