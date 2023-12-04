import 'login_event.dart';
import 'login_state.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class LoginBloc extends Bloc<LoginEvent, LoginState> {
  LoginBloc() : super(LoginInitial()) {
    on<ChangeTab>((event, emit) {
      emit(LoginState(
        currentTabIndex: event.tabIndex,
      ));
    });
  }
}
