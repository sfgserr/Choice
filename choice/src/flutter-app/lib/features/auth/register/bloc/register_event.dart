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

final class SignUpTap extends RegisterEvent {
  final String password;
  final String email;
  final String name;
  final String? surname;

  SignUpTap({
    required this.password,
    required this.email,
    required this.name,
    this.surname,
  });
}

final class ClientRegister extends RegisterEvent {}

final class CompanyRegister extends RegisterEvent {}
