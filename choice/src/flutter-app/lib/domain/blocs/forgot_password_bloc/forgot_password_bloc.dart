import 'package:flutter_bloc/flutter_bloc.dart';
import 'forgot_password_event.dart';
import 'forgot_password_state.dart';

class ForgotPasswordBloc
    extends Bloc<ForgotPasswordEvent, ForgotPasswordState> {
  ForgotPasswordBloc() : super(ForgotPasswordInitial()) {
    on<EnableMainBtn>((event, emit) {
      emit(
        ForgotPasswordState(isEnabledMainBtn: event.isEnabledMainBtn),
      );
    });
  }
}
