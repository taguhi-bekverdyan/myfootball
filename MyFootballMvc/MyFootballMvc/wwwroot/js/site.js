// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Mark main navbar menu item as active if subnav item is selected
$tournamentMenuItem = $('.tournament-menu-item');
if ($('.league-name').length > 0 && !$tournamentMenuItem.hasClass('active')) {
  $tournamentMenuItem.addClass('active');
}