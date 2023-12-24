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

final class UpdateSecondPassword extends SetNewPasswordEvent {
  final String newPassword;

  UpdateSecondPassword({
    required this.newPassword,
  });
}
