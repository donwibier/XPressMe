(function () {
	 "use strict"; 
	 window.mainMenuItemClick = function (s, e) {
	 	 var wnd = dxdash.GetWindow(0);
	 	 if (e.item.name !== '') {
	 	 	 if (!dxdash.IsWindowVisible(wnd)) {
	 	 	 	 dxdash.PerformWindowCallback(wnd, e.item.name);
	 	 	 }
	 	 	 else {
	 	 	 	 dxdash.HideWindow(wnd);
	 	 	 }
	 	 	 e.htmlEvent.cancelBubble = true;
	 	 	 e.processOnServer = false;
	 	 	 return false;
	 	 }
	 };

	 window.popupCtrlInit=function(s, e) {
	 	 $(window).bind('orientationchange resize', function() { 													
	 	 	 var wnd = dxdash.GetWindow(0);
	 	 	 if (s.IsWindowVisible(wnd)) {
	 	 	 	 s.SetWindowLeft(0, $('#navContainer').offset().left);
	 	 	 	 s.SetWindowTop(0, $('#navContainer').offset().top+$('#navContainer').height());														
	 	 	 	 s.SetWindowSize(0, $('#navContainer').width(), s.GetWindowHeight(wnd));
	 	 	 }
	 	 });
	 };

	 window.popupCtrlShown = function (s, e) {
	 	 s.SetWindowSize(e.window, $('#navContainer').width(), s.GetWindowHeight(e.window));
	 	 $('.dxbButton_Moderno').filter('.dxbButtonSys').css('height', '');
	 };

	 window.popupCtrlEndCallback = function (s, e) {
	 	 var wnd = s.GetWindow(0);
	 	 
	 	 if (s.IsWindowVisible(wnd)) {
	 	 	 __aspxControlCollection.ForEachControl(function (ctrl, ctx) {
	 	 	 	 if ((typeof ctrl.cpCallbackCmd !== 'undefined') && (typeof ctrl.PerformCallback !== 'undefined')) {
	 	 	 	 	 ctrl.PerformCallback(ctrl.cpCallbackCmd);
	 	 	 	 }
	 	 	 }, null);
	 	 	 dxdash.HideWindow(wnd);
	 	 }
	 	 else {
	 	 	 dxdash.ShowWindowAtPos(wnd, $('#navContainer').offset().left, $('#navContainer').offset().top + $('#navContainer').height());
	 	 }
	 };

	 window.PostDataViewEndCallback = function (s, e) {
	 	 $('.dxdvItem_Moderno').css('height', 0);
	 };
	 window.PostDataViewInit = function (s, e) {
	 	 $('.dxdvItem_Moderno').css('height', 0);
	 };

	 window.popupCancelButtonClick = function (s, e) {
	 	 var wnd = dxdash.GetWindow(0);
	 	 if (dxdash.IsWindowVisible(wnd)) {
	 	 	 dxdash.HideWindow(wnd);
	 	 }
	 };

	 window.popupOkButtonClick = function (s, e) {
	 	 var wnd = dxdash.GetWindow(0);
	 	 if (dxdash.IsWindowVisible(wnd)) {
	 	 	 dxdash.PerformWindowCallback(wnd, 'Store');
	 	 }
	 };

})(jQuery);