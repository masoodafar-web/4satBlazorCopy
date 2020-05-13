$('#StartDate1').change(function () {
	var startdate = $(this).val();
	var Res = moment.from(startdate, 'fa', 'YYYY/MM/DD').format('YYYY/MM/DD');
	if (Res == "Invalid date") {
		$('#StartDate').val("").trigger("change");

	} else {
		$('#StartDate').val(Res).trigger("change");

	}
});
$('#EndDate1').change(function () {
	var startdate = $(this).val();
	var Res = moment.from(startdate, 'fa', 'YYYY/MM/DD').format('YYYY/MM/DD');
	if (Res == "Invalid date") {
		$('#EndDate').val("").trigger("change");

	} else {
		$('#EndDate').val(Res).trigger("change");

	}
});
$(document).ready(function () {
	$('#StartDate').val("").trigger("change");
});