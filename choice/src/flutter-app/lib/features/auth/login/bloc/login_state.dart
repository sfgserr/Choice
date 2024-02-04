part of 'login_bloc.dart';

class LoginState extends Equatable {
  final int currentTabIndex;
  final bool isLoginBtnEnabled;
  final bool isGettingCode;
  final bool isObscurePassword;

  const LoginState({
    required this.isObscurePassword,
    required this.isGettingCode,
    required this.currentTabIndex,
    required this.isLoginBtnEnabled,
  });

  @override
  List<Object> get props => [
        currentTabIndex,
        isLoginBtnEnabled,
        isGettingCode,
        isObscurePassword,
      ];
}

class LoginInitial extends LoginState {
  const LoginInitial()
      : super(
          currentTabIndex: 0,
          isLoginBtnEnabled: false,
          isGettingCode: false,
          isObscurePassword: true,
        );
}

class LoginLoading extends LoginState {
  const LoginLoading({
    required super.isObscurePassword,
    required super.isGettingCode,
    required super.currentTabIndex,
    required super.isLoginBtnEnabled,
  });
}

class LoginFailure extends LoginState {
  final String error;

  const LoginFailure({
    required this.error,
    required super.isObscurePassword,
    required super.isGettingCode,
    required super.currentTabIndex,
    required super.isLoginBtnEnabled,
  });

  @override
  List<Object> get props => [
        error,
        currentTabIndex,
        isLoginBtnEnabled,
        isGettingCode,
        isObscurePassword,
      ];

  @override
  String toString() => 'LoginFailure { error: $error }';
}
