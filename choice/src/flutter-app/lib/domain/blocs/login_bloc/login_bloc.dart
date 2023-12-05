import 'login_event.dart';
import 'login_state.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class LoginBloc extends Bloc<LoginEvent, LoginState> {
  LoginBloc() : super(LoginInitial()) {
    on<ChangeTab>((event, emit) {
      emit(LoginState(
        currentTabIndex: event.tabIndex,
        isLoginBtnEnabled: state.isLoginBtnEnabled,
        isGettingCode: state.isGettingCode,
      ));
    });

    on<EnableLoginBtn>((event, emit) {
      emit(LoginState(
        currentTabIndex: state.currentTabIndex,
        isLoginBtnEnabled: event.isLoginBtnEnabled,
        isGettingCode: state.isGettingCode,
      ));
    });

    on<GetCode>((event, emit) {
      emit(LoginState(
        currentTabIndex: 1, // phone tab
        isLoginBtnEnabled: false,
        isGettingCode: true,
      ));
    });
  }
}

void getStateStatus(dynamic state) {
  print('-------');
  print('[NEW STATE]');
  print(state.currentTabIndex);
  print(state.isLoginBtnEnabled);
  print(state.isGettingCode);
}