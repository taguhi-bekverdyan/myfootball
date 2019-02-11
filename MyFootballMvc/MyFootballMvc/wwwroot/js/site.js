// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Mark main navbar menu item as active if subnav item is selected
$tournamentMenuItem = $('.tournament-menu-item');
if ($('.league-name').length > 0 && !$tournamentMenuItem.hasClass('active')) {
  $tournamentMenuItem.addClass('active');
}

// Common ajax submit function for MyProfiles tabs
function AjaxJsonSumbitWithConfirm(url, object, form) {
  bootbox.confirm("Do you want to save changes?", function (result) {
    if (result) {
      $.ajax({
        url: url,
        method: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(object),
        success: function () {
          var newTxt = 'Save';
          var btn = $(form).find('button[type="submit"]');
          if (btn.text() !== newTxt) {

            // Change submit button text to 'Save'
            btn.replaceWith(btn.text(newTxt));

            // Add 'Add new pitch' button
            $(form).append('<a class="btn btn-primary add-new-item" href="/Pitch/Create">Add new pitch</a>');
          }

          bootbox.alert("The changes have been done");
        }
      });
    }
  });
}

$(document).ready(function () {
    $('#all-clubs-table, #all-managers-table, #all-players-table, #pitch-table, #referees-table, #all-referees-table, #players-table,#clubs-table, #managers-table').DataTable();
});