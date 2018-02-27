<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View_UpdateAssets.ascx.cs" Inherits="Hydros.View_UpdateAssets" %>

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
        var tblPhoto = $('#tblPhotoList').DataTable({
            paging: false,
            searching: false,
            ordering: false,
            info: false,
            ajax: {
                url: '/DesktopModules/Hydros/Api/Asset/PhotoGetByItem?ItemID=<%= ItemID %>',
                dataSrc: '',
                async: false
            },
            columns: [
                { data: 'AssetID' },
                { data: 'IsPrimary' },
                { data: 'IsDeleted' }
            ],
            columnDefs: [
                 {
                     render: function (data, type, row) {
                         return '<img src="' + row.URLPhoneThumb + '" style="height: 80px;" />';
                     },
                     targets: 0
                 },
                 {
                     render: function (data, type, row) {
                         return '<input type="radio" name="rdiCheck" value="' + row.AssetID + '">' +
                                '<input type="hidden" name="hdfIsPrimary" value="' + row.IsPrimary + '" id="' + row.AssetID + '">';
                     },
                     className: 'dt-center',
                     targets: 1
                 },
                 {
                     render: function (data, type, row) {
                         return '<input type="checkbox" name="chbCheck" value="' + row.AssetID + '">';
                     },
                     className: 'dt-center',
                     targets: 2
                 },
            ]
        });

     

        //DropZone
        $(function () {
            var myDropzone = new Dropzone("#dropzone", {
                url: '/file/post',
                uploadMultiple: true,
                acceptedFiles: "image/*",
                previewsContainer: false
            });
            //How to input GroupID for B1398 ex
            myDropzone.on("successmultiple", function (files) {
             //   var ext = (files.name).eplist('.')[1];
              //  if (ext != "jpg" || ext != "jpeg" || ext != "png") {

                //    
                //get ItemType
                var ItemType = $('#sltItemType').val();

                $.fn.UploadFile('<%= ItemID %>', '<%= GroupID %>', ItemType, '<%= AssetType %>', files);
                //refesh PopUp

                //console.log(files.length);
            });
        });

        //Upload Files
        $.fn.UploadFile = function (ItemID, GroupID, ItemType, AssetType, Files) {
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
                            tblPhoto.ajax.reload();
                            $.fn.CheckPrimary();
                            $.fn.RadioChangeEvent();
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


        //Set Primary Image Check

        $.fn.CheckPrimary = function () {
            $('input[type=hidden][name=hdfIsPrimary]').each(function () {
                if (this.value == "1") {
                    var _HiddenFieldID = $(this).prop('id');
                    $('input[type=radio][name=rdiCheck]').each(function () {
                        if ($(this).val() == _HiddenFieldID) {
                            $(this).prop("checked", "checked");
                            var _rdiValue = this.value;
                            $('input[type=checkbox][name=chbCheck]').each(function () {
                                //Check if radio and checkbox are the same value
                                if ($(this).val() == _rdiValue) {
                                    $(this).prop("disabled", "disabled");
                                    $(this).prop("checked", "");
                                }
                                else {
                                    $(this).prop("disabled", "");
                                }
                            });
                        }
                    });
                }
            });
        }

        //Call Check Primary for the firsttime load
        $.fn.CheckPrimary();

        //Update Primary and Delete Image
        $("#btnUpdate").click(function (e) {
            //Update Primary Image
            $('input[type=radio][name=rdiCheck]').each(function () {
                if ($(this).prop('checked') == true) {
                    var data = new FormData();
                    var AssetID = $(this).val();
                    var ItemType = $('#sltItemType').val();

                    data.append('AssetID', AssetID);
                    data.append('ItemID', "<%= ItemID %>");
                    data.append('AssetType', "<%= AssetType %>");
                    data.append('ItemType', ItemType);

                    //Call API update
                    $.ajax({
                        type: 'POST',
                        url: '/DesktopModules/Hydros/Api/Asset/UpdatePrimary',
                        data: data,
                        contentType: false,
                        processData: false,
                        success: function (data) {

                            if (data = 201) {
                                //  alert("successfull");
                                e.preventDefault();
                            }
                            else {
                                alert("Fail!");
                            }
                        },
                        error: function (e) {con
                            alert(e);
                        }
                    });
                }
            });

            //Update Delete Image
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
            // window.opener.location.reload
            dnnModal.closePopUp(true);
          //  window.close();
           // setTimeout("window.open(self.location, '_self');", 1000);
        });


        //Radio change event
        $.fn.RadioChangeEvent = function () {
            $('input[type=radio][name=rdiCheck]').change(function () {
                if (this.checked) {
                    var _rdiValue = this.value;
                    $('input[type=checkbox][name=chbCheck]').each(function () {
                        //Check if radio and checkbox are the same value
                        if ($(this).val() == _rdiValue) {
                            $(this).prop("disabled", "disabled");
                            $(this).prop("checked", "");
                        }
                        else {
                            $(this).prop("disabled", "");
                        }
                    });
                }


            });
        }
        //Call for the firsttime load
        $.fn.RadioChangeEvent();

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

        jQuery(function ($) {
            $(document).ajaxStart(function () {
                $("#ajax_loader").show();
                $("#dropzone").css("opacity", 0.2);
            }).ajaxStop(function () {
                $("#ajax_loader").hide();
                $("#dropzone").css("opacity", 1);
            });
        });

    });
</script>

<div id="dropzone" class="DropArea">
    <%--<input id="fileUpload" name="file" type="file" multiple />--%>
    Drop files here or click to upload. 
    <h4>
        <span class="label label-default"><%= ItemID %></span>
        <span class="label label-default"><%= AssetType %></span>
    </h4>
    <select class="form-control" id="sltItemType">
        <option value='Camera'>Camera</option>
        <option value='LineArt'>Line Art</option>
    </select>
    <table id="tblPhotoList" class="table table-hover">
        <thead>
            <tr>
                <th class="text-center">Photo</th>
                <th class="text-center">Set Primary Photo</th>
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



