$('#first').show();
$('#second').hide();
$('#third').hide();
$('#fourth').hide();
$('span').click(function () {
    if ($(this).hasClass('first')) {
        $('#nprogress-bar').val('0');
        $(this).nextAll().removeClass('border-change');
        //$('.percent').html("0% Complete");
        $('#first').show();
        $('#second').hide();
        $('#third').hide();
        $('#fourth').hide();
    } else if ($(this).hasClass('second')) {
        $(this).nextAll().removeClass('border-change');
        $('#nprogress-bar').val('34');
        $(this).prevAll().addClass('border-change');
        $(this).addClass('border-change');
        //$('.percent').html("33% Complete");
        $('#second').show();
        $('#first').hide();
        $('#third').hide();
        $('#fourth').hide();
    } else if ($(this).hasClass('third')) {
        $(this).nextAll().removeClass('border-change');
        $('#nprogress-bar').val('67');
        $(this).prevAll().addClass('border-change');
        $(this).addClass('border-change');
        //$('.percent').html("66% Complete");
        $('#second').hide();
        $('#first').hide();
        $('#fourth').hide();
        $('#third').show();
    } else {
        $('#nprogress-bar').val('100');
        $(this).addClass('border-change');
        $(this).prevAll().addClass('border-change');
        //$('.percent').html("100% Complete");
        $('#third').hide();
        $('#first').hide();
        $('#fourth').show();
    }
});