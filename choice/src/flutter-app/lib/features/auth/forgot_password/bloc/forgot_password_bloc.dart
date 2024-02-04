import 'dart:async';

import 'package:flutter_bloc/flutter_bloc.dart';
part 'forgot_password_event.dart';
part 'forgot_password_state.dart';

class ForgotPasswordBloc
    extends Bloc<ForgotPasswordEvent, ForgotPasswordState> {
  ForgotPasswordBloc() : super(ForgotPasswordInitial()) {
    on<EnableMainBtn>((event, emit) {
      emit(
        ForgotPasswordState(
          isEnabledMainBtn: event.isEnabledMainBtn,
          isEmailView: state.isEmailView,
          currentEmail: state.currentEmail,
          remainSeconds: state.remainSeconds,
        ),
      );
    });

    on<ChangeView>((event, emit) {
      emit(
        ForgotPasswordState(
          isEnabledMainBtn: false,
          isEmailView: event.isEmailView,
          currentEmail: event.currentEmail,
          remainSeconds: 0,
        ),
      );
    });

    on<UpdateTimer>((event, emit) {
      int remainSeconds = event.remainSeconds;
      Timer _timer = Timer.periodic(
        const Duration(seconds: 1),
            (timer) {
          emit(
            ForgotPasswordState(
              isEnabledMainBtn: state.isEnabledMainBtn,
              isEmailView: state.isEmailView,
              currentEmail: state.currentEmail,
              remainSeconds: remainSeconds - timer.tick,
            ),
          );
        },
      );
    });

  }
}