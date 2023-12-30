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
        isObscurePassword: state.isObscurePassword,
      ));
    });

    on<EnableLoginBtn>((event, emit) {
      emit(LoginState(
        currentTabIndex: state.currentTabIndex,
        isLoginBtnEnabled: event.isLoginBtnEnabled,
        isGettingCode: state.isGettingCode,
        isObscurePassword: state.isObscurePassword,
      ));
    });

    on<GetCode>((event, emit) {
      bool isGettingCode = event.isGettingCode;

      emit(LoginState(
        currentTabIndex: isGettingCode ? 1 : 0, // phone tab
        isLoginBtnEnabled: false,
        isGettingCode: isGettingCode,
        isObscurePassword: state.isObscurePassword,
      ));
    });

    on<ObscurePasswordText>((event, emit) {
      emit(LoginState(
        currentTabIndex: state.currentTabIndex, // phone tab
        isLoginBtnEnabled: state.isLoginBtnEnabled,
        isGettingCode: state.isLoginBtnEnabled,
        isObscurePassword: !state.isObscurePassword,
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