part of 'set_new_password_bloc.dart';

sealed class SetNewPasswordEvent {}

final class EnableMainBtn extends SetNewPasswordEvent {
  final bool isMainBtnEnabled;

  EnableMainBtn({required this.isMainBtnEnabled});
}

final class ObscurePasswordText extends SetNewPasswordEvent {}

final class ObscureConfirmPasswordText extends SetNewPasswordEvent {}

final class UpdateFirstPassword extends SetNewPasswordEvent {
  final String newPassword;

  UpdateFirstPassword({
    required this.newPassword,
  });
}

