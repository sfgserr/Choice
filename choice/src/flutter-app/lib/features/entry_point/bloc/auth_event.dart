part of 'auth_bloc.dart';

sealed class AuthEvent extends Equatable {
  const AuthEvent();

  @override
  List<Object> get props => [];
}

final class AppStarted extends AuthEvent {}

final class LoggedIn extends AuthEvent {
  // final String token;
  //
  // const LoggedIn({required this.token});

  @override
  String toString() => 'LoggedIn { token: no_token }';
}

final class LoggedOut extends AuthEvent {}