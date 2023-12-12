import 'package:flutter_bloc/flutter_bloc.dart';
import 'set_new_password_event.dart';
import 'set_new_password_state.dart';

class SetNewPasswordBloc
    extends Bloc<SetNewPasswordEvent, SetNewPasswordState> {
  SetNewPasswordBloc() : super(SetNewPasswordInitial()) {
    on<EnableMainBtn>((event, emit) {
      emit(
        SetNewPasswordState(
          isMainBtnEnabled: event.isMainBtnEnabled,
          isObscurePassword: state.isObscurePassword,
          isObscureConfirmPassword: state.isObscureConfirmPassword,
        ),
      );
    });

    on<ObscurePasswordText>((event, emit) {
      emit(
        SetNewPasswordState(
          isObscureConfirmPassword: state.isObscureConfirmPassword,
          isObscurePassword: !state.isObscurePassword,
          isMainBtnEnabled: state.isMainBtnEnabled,
        ),
      );
    });

    on<ObscureConfirmPasswordText>((event, emit) {
      emit(
        SetNewPasswordState(
          isObscureConfirmPassword: !state.isObscureConfirmPassword,
          isObscurePassword: state.isObscurePassword,
          isMainBtnEnabled: state.isMainBtnEnabled,
        ),
      );
    });
  }
}
