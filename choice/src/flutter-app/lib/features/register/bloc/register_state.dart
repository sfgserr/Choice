part of 'register_bloc.dart';

class RegisterState {
  final bool isMainBtnEnabled;
  final bool isObscurePassword;
  final bool isObscureConfirmPassword;
  final String firstPassword;
  final String secondPassword;

  RegisterState({
    required this.isObscurePassword,
    required this.isObscureConfirmPassword,
    required this.isMainBtnEnabled,
    required this.firstPassword,
    required this.secondPassword,
  });
}

class RegisterInitial extends RegisterState {
  RegisterInitial()
      : super(
    isMainBtnEnabled: false,
    isObscurePassword: true,
    isObscureConfirmPassword: true,
    firstPassword: '',
    secondPassword: '',
  );
}
