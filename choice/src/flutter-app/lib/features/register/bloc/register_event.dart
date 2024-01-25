part of 'register_bloc.dart';

sealed class RegisterEvent {}


final class EnableMainBtn extends RegisterEvent {
  final bool isMainBtnEnabled;

  EnableMainBtn({required this.isMainBtnEnabled});
}

final class ObscurePasswordText extends RegisterEvent {}

final class ObscureConfirmPasswordText extends RegisterEvent {}

final class UpdateFirstPassword extends RegisterEvent {
  final String newPassword;

  UpdateFirstPassword({
    required this.newPassword,
  });
}

final class UpdateSecondPassword extends RegisterEvent {
  final String newPassword;

  UpdateSecondPassword({
    required this.newPassword,
  });
}
