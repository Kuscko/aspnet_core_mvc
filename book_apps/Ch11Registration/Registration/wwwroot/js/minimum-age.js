jQuery.validator.addMethod("minimumage", function(value, element, param) {
	if (value === '') return false;

	var dateToCheck = new Date(value);
	if (dateToCheck === "Invalid Date") return false;

	var minYears = Number(param);

	dateToCheck.setFullYear(dateToCheck.getFullYear() + minYears);

	var today = new Date();
	return (dateToCheck <= today);
});

jQuery.validator.unobtrusive.adapters.addSingleVal("minimumage", "years");
