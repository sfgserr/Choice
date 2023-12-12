sealed class SetNewPasswordEvent {}

final class EnableMainBtn extends SetNewPasswordEvent {
  final bool isMainBtnEnabled;

  EnableMainBtn({required this.isMainBtnEnabled});
}

final class ObscurePasswordText extends SetNewPasswordEvent {}

final class ObscureConfirmPasswordText extends SetNewPasswordEvent {}
