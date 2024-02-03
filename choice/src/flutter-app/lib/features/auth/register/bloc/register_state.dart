part of 'register_bloc.dart';

class RegisterState extends Equatable {
  final bool isMainBtnEnabled;
  final bool isObscurePassword;
  final bool isObscureConfirmPassword;
  final String firstPassword;
  final bool isCompanyRegister;

  const RegisterState({
    required this.isObscurePassword,
    required this.isObscureConfirmPassword,
    required this.isMainBtnEnabled,
    required this.firstPassword,
    required this.isCompanyRegister,
  });

  @override
  List<Object> get props => [
    isObscurePassword,
    isObscureConfirmPassword,
    isMainBtnEnabled,
    firstPassword,
    isCompanyRegister,
  ];
}

class RegisterInitial extends RegisterState {
  const RegisterInitial()
      : super(
          isMainBtnEnabled: false,
          isObscurePassword: true,
          isObscureConfirmPassword: true,
          firstPassword: '',
          isCompanyRegister: false,
        );
}

final class RegisterLoading extends RegisterState {
  const RegisterLoading({
    required super.isObscurePassword,
    required super.isObscureConfirmPassword,
    required super.isMainBtnEnabled,
    required super.firstPassword,
    required super.isCompanyRegister,
  });
}

final class RegisterFailure extends RegisterState {
  final String error;

  const RegisterFailure({
    required this.error,
    required super.isObscurePassword,
    required super.isObscureConfirmPassword,
    required super.isMainBtnEnabled,
    required super.firstPassword,
    required super.isCompanyRegister,
  });


  @override
  List<Object> get props => [
    error,
    isObscurePassword,
    isObscureConfirmPassword,
    isMainBtnEnabled,
    firstPassword,
    isCompanyRegister,
  ];

  @override
  String toString() => 'RegisterFailure { error: $error }';
}
