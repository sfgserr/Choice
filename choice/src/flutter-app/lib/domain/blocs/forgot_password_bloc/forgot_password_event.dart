
abstract class ForgotPasswordEvent {}

class EnableMainBtn extends ForgotPasswordEvent {
  final bool isEnabledMainBtn;
  EnableMainBtn({required this.isEnabledMainBtn});
}