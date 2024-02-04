part of 'forgot_password_bloc.dart';

sealed class ForgotPasswordEvent {}

final class EnableMainBtn extends ForgotPasswordEvent {
  final bool isEnabledMainBtn;

  EnableMainBtn({this.isEnabledMainBtn = false});
}

final class ChangeView extends ForgotPasswordEvent {
  final bool isEmailView;
  final String currentEmail;

  ChangeView({
    this.isEmailView = true,
    required this.currentEmail,
  });
}

final class UpdateTimer extends ForgotPasswordEvent {
  final int remainSeconds;

  UpdateTimer({
    required this.remainSeconds,
  });
}

final class ResetPassword extends ForgotPasswordEvent {
  final bool isResetPasswordView;

  ResetPassword({
    required this.isResetPasswordView,
  });
}
