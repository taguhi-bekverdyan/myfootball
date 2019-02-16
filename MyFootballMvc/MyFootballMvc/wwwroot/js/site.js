// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


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

  // Mark main navbar menu item as active if subnav item is selected
  $tournamentMenuItem = $('.tournament-menu-item');
  if ($('.league-name').length > 0 && !$tournamentMenuItem.hasClass('active')) {
    $tournamentMenuItem.addClass('active');
  }

  var $colorpickers = $('#cp1, #cp2, #cp3');
  if ($colorpickers.length > 0) {
    $('#cp1, #cp2, #cp3').colorpicker();
  }

  var $tables = $('#all-clubs-table, #all-managers-table, #all-players-table, #pitch-table, #referees-table, #all-referees-table, #players-table, #clubs-table, #managers-table');
  if ($tables.length > 0) {
    $($tables).DataTable();
  }

  // Edit.cshtml view page
  $("#js-playerForm").submit(function (e) {
    e.preventDefault();

    var position = $("#js-position").val();
    var foot = $("#js-foot").val();
    var height = $("#js-height").val();
    var weight = $("#js-weight").val();
    var playerStatus = $("#js-player-status-hidden").val();
    var playerAvatarImagePath = $("#js-hidden-for-player-image").val();

    var ps = { Foot: foot, Height: height, Weight: weight };

    var player = { Position: position, PhysicalStats: ps, PlayerStatus: playerStatus, Avatar: playerAvatarImagePath };

    AjaxJsonSumbitWithConfirm("Player/", player, this);
  });

  $("#js-coachForm").submit(function (e) {
    e.preventDefault();
    var license = $("#js-coach-license").val();
    var coachStatus = $("js-coach-status-hidden").val();
    var coachAvatarImagePath = $("#js-hidden-for-coach-image").val();

    var coach = { License: license, CoachStatus: coachStatus, Avatar: coachAvatarImagePath };

    AjaxJsonSumbitWithConfirm("Coach/", coach, this);
  });

  $("#js-staffForm").submit(function (e) {
    e.preventDefault();

    var license = $("#js-staff-license").val();
    var occupation = $("#js-staff-occupation").val();
    var staffStatus = $("js-staff-status-hidden").val();
    var staffAvatarImagePath = $("#js-hidden-for-staff-image").val();

    var staff = { License: license, Occupation: occupation, StaffStatus: staffStatus, Avatar: staffAvatarImagePath };

    AjaxJsonSumbitWithConfirm("Staff/", staff, this);
  });

  $("#js-refereeForm").submit(function (e) {
    e.preventDefault();

    var license = $("#js-referee-license").val();
    var refereeAvatarImagePath = $("#js-hidden-for-referee-image").val();

    var referee = { License: license, Avatar: refereeAvatarImagePath };

    AjaxJsonSumbitWithConfirm("Referee/", referee, this);
  });

  $("#js-landlordForm").submit(function (e) {
    e.preventDefault();

    var organization = $("#js-landlord-organization").val();
    var phoneNumber = $("#js-landlord-phone-number").val();
    var landlord = {
      Organization: organization,
      PhoneNumber: phoneNumber
    };

    AjaxJsonSumbitWithConfirm("Landlord/", landlord, this);
  });

  function filePreview(input, owner) {
    if (input.files && input.files[0]) {
      var reader = new FileReader();
      reader.onload = function (e) {
        $("#js-" + owner + "-image").attr("src", e.target.result);
        $("#js-hidden-for-" + owner + "-image").val(e.target.result);
      };
      reader.readAsDataURL(input.files[0]);
    }
  }

  $("#js-player-image-button").change(function () {
    filePreview(this, "player");
  });
  $("#js-coach-image-button").change(function () {
    filePreview(this, "coach");
  });
  $("#js-staff-image-button").change(function () {
    filePreview(this, "staff");
  });
  $("#js-referee-image-button").change(function () {
    filePreview(this, "referee");
  });

});