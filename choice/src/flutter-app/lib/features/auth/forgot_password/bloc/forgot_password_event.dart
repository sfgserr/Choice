part of 'forgot_password_bloc.dart';

sealed class ForgotPasswordEvent extends Equatable {

  @override
  List<Object> get props => [];
}

final class EnableMainBtn extends ForgotPasswordEvent {
  final bool isEnabledMainBtn;

  EnableMainBtn({this.isEnabledMainBtn = false});

  @override
  List<Object> get props => [isEnabledMainBtn];
}

final class ChangeView extends ForgotPasswordEvent {
  final bool isEmailView;
  final String email;

  ChangeView({
    this.isEmailView = true,
    required this.email,
  });

  @override
  List<Object> get props => [isEmailView, email];
}

final class SendCodeTap extends ForgotPasswordEvent {
  final String email;
  final bool isEmailView;

  SendCodeTap({
    required this.email,
    required this.isEmailView,
  });

  @override
  List<Object> get props => [isEmailView, email];
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

  @override
  List<Object> get props => [isResetPasswordView];
}
