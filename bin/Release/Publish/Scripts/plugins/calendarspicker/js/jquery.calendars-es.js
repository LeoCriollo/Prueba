﻿/* http://keith-wood.name/calendars.html
   Spanish localisation for Gregorian/Julian calendars for jQuery.
   Traducido por Vester (xvester@gmail.com). */
(function($) {
	'use strict';
	$.calendars.calendars.gregorian.prototype.regionalOptions.es = {
		name: 'Gregorian',
		epochs: ['BCE', 'CE'],
		monthNames: ['Enero','Febrero','Marzo','Abril','Mayo','Junio',
		'Julio','Agosto','Septiembre','Octubre','Noviembre','Diciembre'],
		monthNamesShort: ['Ene','Feb','Mar','Abr','May','Jun',
		'Jul','Ago','Sep','Oct','Nov','Dic'],
		dayNames: ['Domingo','Lunes','Martes','Miércoles','Jueves','Viernes','Sábado'],
		dayNamesShort: ['Dom','Lun','Mar','Mié','Juv','Vie','Sáb'],
		dayNamesMin: ['D','L','M','M','J','V','S'],
		digits: null,
		dateFormat: 'dd/mm/yyyy',
		firstDay: 0,
		isRTL: false
	};
	if ($.calendars.calendars.julian) {
		$.calendars.calendars.julian.prototype.regionalOptions.es =
			$.calendars.calendars.gregorian.prototype.regionalOptions.es;
	}
})(jQuery);
