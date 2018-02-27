<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View_ManageAssets.ascx.cs" Inherits="Hydros.View_ManageAssets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

<link href="//cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
<script src="//cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>

<script>
$(document).ready(function(){
    //DataTable
    var url = window.location.href;
    var wHeight = $(window).height() - 100;
    var wWidth = $(window).width() / 3;
    var moduleID = <%= ModuleId %>;
    var tableAsset = $('#tblAssets').DataTable({
        processing: true,
        serverSide: true,
        ajax: {
            url: '/DesktopModules/Hydros/Api/Asset/Summary',
            dataSrc: 'data',
            type: "POST",
            datatype: "json"
        },
        columns : [
            { data: 'ItemID' },
            { data: 'ItemType' },
            { data: 'Status'},
            { data: 'LatestModifiedDate' },
            { data: 'TotalPhoto' },
            { data: 'TotalVideo' },
            { data: 'TotalDocument' }
        ],
        columnDefs : [
            {
                render: function (data, type, row) {
                    var value = '';
                    if(data === null){
                        value = 'None';
                    }
                    else
                    {
                        value = data;
                    }
                    var itemID = row.ItemID;
                    var PopUrl = "return dnnModal.show('" + url + "/ctl/UpdateStatus/mid/" + moduleID + "/ItemID/" + itemID + "?popUp=true',true," + wHeight + "," + wWidth + ",false,'')";
                    return '<button onclick="' + PopUrl + '" class="btn ' + GetStatusClass(value) + '">' + value + '</button>';
                },
                targets: 2
            },
            {
                render: function (data, type, row) {
                    var itemID = row.ItemID;
                    var itemType = row.ItemType;
                    //Chua xu ly duoc URL Master
                    var PopUrl = "return dnnModal.show('" + url + "/ctl/Update/mid/" + moduleID + "/ItemID/" + itemID + "/AssetType/Photo" + "?popUp=true',true," + wHeight + "," + wWidth + ",false,'')";
                    return '<button class="btn btn-info" onclick="' + PopUrl + '">Photo <span class="badge">' + data + '</span></button>';
                },
                targets: 4
            },
            {
                render: function (data, type, row) {
                    var itemID = row.ItemID;
                    var itemType = row.ItemType;
                    //Chua xu ly duoc URL Master
                    var PopUrl = "return dnnModal.show('" + url + "/ctl/UpdateVideo/mid/" + moduleID + "/ItemID/" + itemID + "/AssetType/Video" + "?popUp=true',true," + wHeight + "," + wWidth + ",false,'')";
                    return '<button class="btn btn-success" onclick="' + PopUrl + '">Video <span class="badge">' + data + '</span></button>';
                },
                targets: 5
            },
            {
                render: function (data, type, row) {
                    var itemID = row.ItemID;
                    var itemType = row.ItemType;
                    //Chua xu ly duoc URL Master
                    var PopUrl = "return dnnModal.show('" + url + "/ctl/UpdateDocument/mid/" + moduleID + "/ItemID/" + itemID + "/AssetType/Document" + "?popUp=true',true," + wHeight + "," + wWidth + ",false,'')";
                    return '<button class="btn btn-warning" onclick="' + PopUrl + '">Document <span class="badge">' + data + '</span></button>';
                },
                targets: 6
            }

        ]
    });

    function GetStatusClass(status)
    {
    //.btn-default
    //.btn-primary
    //.btn-success
    //.btn-info
    //.btn-warning
    //.btn-danger

        if(status == 'Dropped' || status == 'Removed')
            return 'btn-danger';
        if(status == 'None')
            return 'btn-primary';
        if(status == 'New')
            return 'btn-success';
        if(status == 'Special')
            return 'btn-warning';
    }

    //Open PopUp Upload
    var wUploadHeight = $(window).height() - 100;
    var wUPloadWidth = $(window).width() / 2 + 100;
    var PopUrl = "return dnnModal.show('" + url + "/ctl/Upload/mid/" + moduleID + "?popUp=true',true," + wUploadHeight + "," + wUPloadWidth + ",false,'')";
    $('#btOpenUpload').attr('onclick', PopUrl);

    //Re-draw table without filter removed
    $('#drawtbl').click(function (e){
        tableAsset.draw(false);
        e.preventDefault();
    });
   
});

</script>

<h2>Assets Management System</h2>
<button id="btOpenUpload" class="btn btn-success btn-lg">Open Upload</button>
<hr />

<table id="tblAssets" class="table table-hover">
    <thead>
        <tr>
            <th>ItemID</th>
            <th>Product Type</th>
            <th>Status</th>
            <th>Latest Modified Date</th>
            <th>Photos</th>
            <th>Videos</th>
            <th>Documents</th>
        </tr>
    </thead>
</table>

<button id="drawtbl">draw</button>