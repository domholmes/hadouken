var thingId;

var initialise = function (id) {

    thingId = id;
    getThing();

    $('#save').click(saveThing);
};

var getThing = function () {

    $.get('/Thing/GetThing/' + thingId, function (data) {

        updateThing(data);
    });
};

var saveThing = function () {

    $.post("/Thing/SaveThing", $('#thingForm').serialize()).done(function (data) {
    
        updateThing(data);
    });
};

var updateThing = function (data) {

    $('#thing').html(data);

    $('#thingForm').removeData("validator");
    $('#thingForm').removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse('#thingForm');


    if ($('#IsEdited').val() === 'True') {

        $('#revert').show();
    }
    else {

        $('#revert').hide();
    }
};