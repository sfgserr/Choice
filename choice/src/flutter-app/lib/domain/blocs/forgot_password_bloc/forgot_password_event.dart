
abstract class ForgotPasswordEvent {}

class EnableMainBtn extends ForgotPasswordEvent {
  final bool isEnabledMainBtn;
  EnableMainBtn({this.isEnabledMainBtn = false});
}