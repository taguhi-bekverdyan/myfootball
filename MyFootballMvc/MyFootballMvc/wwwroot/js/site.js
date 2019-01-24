// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Mark main navbar menu item as active if subnav item is selected
$tournamentMenuItem = $('.tournament-menu-item');
if ($('.league-name').length > 0 && !$tournamentMenuItem.hasClass('active')) {
  $tournamentMenuItem.addClass('active');
}

// Common ajax submit function for MyProfiles tabs
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

$(document).ready(function () {
  $('#my-pitches-table').DataTable();
});