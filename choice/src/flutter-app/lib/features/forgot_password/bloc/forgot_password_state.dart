class ForgotPasswordState {
  final bool isEnabledMainBtn;
  final bool isEmailView;
  final String? currentEmail;
  final int remainSeconds;

  ForgotPasswordState({
    this.currentEmail,
    required this.remainSeconds,
    this.isEnabledMainBtn = false,
    required this.isEmailView,
  });
}

class ForgotPasswordInitial extends ForgotPasswordState {
  ForgotPasswordInitial()
      : super(
          isEnabledMainBtn: false,
          isEmailView: true,
          remainSeconds: 0,
        );
}
