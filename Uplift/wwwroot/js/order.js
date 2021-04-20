var dataTable;

$(document).ready(function () {
	var url = window.location.search;

	if (url.includes("approved")) {
		loadDataTable("GetAllApprovedStatus");
	}
	else if (url.includes("pending")) {
		loadDataTable("GetAllPendingOrder");
	}
	else {
		loadDataTable("GetAll");
	}
});

function loadDataTable(url) {
	dataTable = $("#tblData").DataTable({
		"ajax": {
			url: "/Admin/Order/" + url,
			type: "Get",
			datatype: "json"
		},
		"columns": [
			{ "data": "name", "width:": "15%" },
			{ "data": "phone", "width:": "15%" },
			{ "data": "email", "width:": "15%" },
			{ "data": "serviceCount", "width:": "15%" },
			{ "data": "status", "width:": "15%" },
			{
				"data": "id",
				"render": function (data) {
					return `<div class="text-center">
								<a href="/Admin/Order/Details/${data}" class="btn btn-success text-white" style='cursor:pointer; width:100px;'>
									<i class='far fa-edit'></i> Details
								</a>
							</div>`;
				}, "width": "25%"
			}
		],
		"language": {
			"emptyTable":"No records found."
		},
		"width":"100%"
	});
}



