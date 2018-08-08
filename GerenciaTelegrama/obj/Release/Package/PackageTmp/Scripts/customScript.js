"use strict";

$('[data-toggle="tooltip"]').tooltip();

function phoneNumber(obj) {
    obj.mask("(99) 9999-9999?9").focusout(function (event) {
        var target, phone, element;
        target = (event.currentTarget) ? event.currentTarget : event.srcElement;
        phone = target.value.replace(/\D/g, '');
        element = $(target);
        element.unmask();
        if (phone.length > 10) {
            element.mask("(99) 99999-999?9");
        } else {
            element.mask("(99) 9999-9999?9");
        }
    });
}

function dateFormat(date) {
    var newDate = new Date();
    newDate.setTime(date.replace('/Date(', '').replace(')/', ''));
    return setZero(newDate.getDate()) + '/' + setZero(newDate.getMonth() + 1) + '/' + newDate.getFullYear() + ' às ' + setZero(newDate.getHours()) + ':' + setZero(newDate.getMinutes());
}

function dateIsValid(datetime) {
    var m = moment(datetime, 'DD/MM/YYYY HH:mm');
    return m.isValid();
}

function phoneNumber(obj) {
    obj.mask("(99) 9999-9999?9").focusout(function (event) {
        var target, phone, element;
        target = (event.currentTarget) ? event.currentTarget : event.srcElement;
        phone = target.value.replace(/\D/g, '');
        element = $(target);
        element.unmask();
        if (phone.length > 10) {
            element.mask("(99) 99999-999?9");
        } else {
            element.mask("(99) 9999-9999?9");
        }
    });
}

function validaDataInicioFim(dataInicio, dataFim) {
    var d1 = new Date(moment(dataInicio, 'DD/MM/YYYY HH:mm').format('YYYY-MM-DD HH:mm'));
    var d2 = new Date(moment(dataFim, 'DD/MM/YYYY HH:mm').format('YYYY-MM-DD HH:mm'));
    return moment(d2).isAfter(d1);
}

function replaceAll(str, needle, replacement) {
    var i = 0;
    while ((i = str.indexOf(needle, i)) != -1) {
        str = str.replace(needle, replacement);
    }
    return str;
}
