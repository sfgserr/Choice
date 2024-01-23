import 'package:choice/repositories/repositories/user_repository.dart';
import 'package:choice/repositories/storage/local_storage.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:get_it/get_it.dart';

part 'auth_state.dart';
part 'auth_event.dart';


class AuthBloc extends Bloc<AuthEvent, AuthState> {
  final UserRepository userRepository = UserRepository();

  AuthBloc() : super(AuthUninitialized()) {
    on<AppStarted>((event, emit) {
      /// with token
      // final bool hasToken = await userRepository.hasToken();
      // if (hasToken) {
      //   yield AuthenticationAuthenticated();
      // } else {
      //   yield AuthenticationUnauthenticated();
      // }

      /// without token
      if (LocalStorage.isLoggedIn) {
        emit(AuthAuthenticated());
      } else {
        emit(AuthUnauthenticated());
      }
    });
    on<LoggedIn>((event, emit) {

      emit(AuthLoading());
      userRepository.logInLocally();
      // await userRepository.persistToken(event.token);
      emit(AuthAuthenticated());
    });
    on<LoggedOut>((event, emit) {
      emit(AuthLoading());
      userRepository.logOutLocally();
      // await userRepository.deleteToken();
      emit(AuthUnauthenticated());
    });
  }
}
