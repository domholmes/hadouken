var thingId;

var initialise = function (id) {

    thingId = id;
    getThing();

    $('#save').click(saveThing);
};

var getThing = function () {

    $.get('/Thing/GetThing/' + thingId, function (data) {

        $('#thing').html(data);
        
        $('#thingForm').removeData("validator");
        $('#thingForm').removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse('#thingForm');

        var isEdited = $('#IsEdited').val();

        if (isEdited === 'True') {
            $('#revert').show();
        }
        else {
            $('#revert').hide();
        }
    });
};

var saveThing = function () {

    $.post("/Thing/SaveThing", $('#thingForm').serialize()).done(function(){
    
        $('#status').text('Thing saved');
    });
};