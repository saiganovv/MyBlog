$(function () {
    if (!Modernizr.inputtypes.dateTime) {
        $("input[type='datetime']").datepicker();
    }
    if (!Modernizr.inputtypes.dateTime) {
        $("input[type='date']").datepicker();
    }
});

//window.onload = function () {
//    var userViewModel = function () {

//        var self = this;
//        self.userId = ko.observable(o);
//        self.UserName = ko.observable('');
//        self.Password = ko.observable('');
//        self.RememberMe = ko.observable(false);
//        self.ConfirmPassword = ko.observable('');

//        self.doLogin = function() {
//            debugger;
//            if ($("#loginForm").valid()) {
//                debugger;
//                $.ajax({
//                    url: "/Account/JsonLogin",
//                    type: "POST",
//                    data: { UserName: self.UserName(), Password: self.Password(), RememberMe: self.RememberMe() },
//                    success: function(response) {

//                        debugger;
//                        if (response.success) {
//                            location = response.redirect || location.href;
//                        } else if (response.errors) {
//                            self.displayErrors($("#loginForm"), response.errors);
//                        }
//                    }
//                });
//            }

//        };
//        self.doRegisterNewUser = function() {
//            debugger;
//            if ($("#registerForm").valid()) {
//                debugger;
//                $.ajax({
//                    url: "/Account/Register",
//                    type: "POST",
//                    data: { UserName: self.UserName(), Password: self.Password(), ConfirmPassword: self.ConfirmPassword() },
//                    success: function(response) {

//                        debugger;
//                        if (response.success) {
//                            location = response.redirect || location.href;
//                        } else if (response.errors) {

//                            self.displayErrors($("#registerForm"), response.errors);
//                        }
//                    }
//                });
//            }
//        };
//        self.getValidationSummaryErrors = function (formobject) {
//            var errorSummary = formobject.find('.validation-summary-errors, .validation-summary-valid');
//            return errorSummary;
//        };

//        self.displayErrors = function (formobject, errors) {
//            var errorSummary = self.getValidationSummaryErrors(formobject)
//                .removeClass('validation-summary-valid')
//                .addClass('validation-summary-errors');

//            var items = $.map(errors, function (error) {
//                return '<li>' + error + '</li>';
//            }).join('');

//            var ul = errorSummary
//                .find('ul')
//                .empty()
//                .append(items);
//        };

//    };
//    ko.applyBindings(new userViewModel());
//}