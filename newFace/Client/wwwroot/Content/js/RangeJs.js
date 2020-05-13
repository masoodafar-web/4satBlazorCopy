function RangeLable(range_input,spanId) {
	// set the label value
	$(range_input)
		.prev("span#"+spanId)
		.html($(range_input).val() + "%");

	// extra verbose calculations to show a solution to labels
	// that become off-center as you get toward the edge of the slider
	var half_thumb_width = 25 / 2;

	var half_label_width =
		$(range_input)
			.prev("span#"+spanId)
			.outerWidth() / 2;

	var slider_width = $(range_input).width();
	var center_position = slider_width / 2;

	var percent_of_range =
		range_input.value / (range_input.max - range_input.min);
	if (isNaN(percent_of_range)) {
		percent_of_range = 0;
	}

	var value_px_position = percent_of_range * slider_width;
	var dist_from_center = value_px_position - center_position;
	var percent_dist_from_center = dist_from_center / center_position;

	var offset = percent_dist_from_center * half_thumb_width;

	var final_label_position = value_px_position - half_label_width - offset;

	var label_position_without_the_offset = value_px_position - half_label_width;

	//$(range_input)
	//    .prev("span#rangAmount")
	//    .css("right", label_position_without_the_offset + "px");
	$(range_input)
		.prev("span#"+spanId)
		.css("right", final_label_position + 15 + "px");
         
}