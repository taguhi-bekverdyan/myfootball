// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Add new pitch submit action 
//$("#js-create-pitch-form").submit(function (e) {
//  e.preventDefault();

//  var pitch = {};

//  $(this).find(".form-control").each(function myfunction() {

//    $this = $(this);
//    let property = $this.attr('name').match(/(?<=Pitch.)\w+/);
//    pitch[property] = $this.val();
//  });

//  AjaxJsonSumbitWithConfirm("/Pitch/Create", pitch);
//});

function AjaxJsonSumbitWithConfirm(url, object) {
  bootbox.confirm("Do you want to save changes?", function (result) {
    if (result) {
      $.ajax({
        url: url,
        method: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(object)
      }).done(function () {
        bootbox.alert("The changes have been done");
      });
    }
  });
}