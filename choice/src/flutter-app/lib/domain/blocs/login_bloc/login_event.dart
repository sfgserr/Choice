
abstract class LoginEvent {}

class ChangeTab extends LoginEvent {
  final int tabIndex;

  ChangeTab({required this.tabIndex});

}