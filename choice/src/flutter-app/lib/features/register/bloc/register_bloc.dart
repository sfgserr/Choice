import 'package:flutter_bloc/flutter_bloc.dart';
part 'register_event.dart';
part 'register_state.dart';


class RegisterBloc extends Bloc<RegisterEvent, RegisterState> {
  RegisterBloc() : super(RegisterInitial()) {
    on<EnableMainBtn>((event, emit) {
      emit(
        RegisterState(
          isMainBtnEnabled: event.isMainBtnEnabled,
          isObscurePassword: state.isObscurePassword,
          isObscureConfirmPassword: state.isObscureConfirmPassword,
          firstPassword: state.firstPassword,
          secondPassword: state.firstPassword,
        ),
      );
    });

    on<ObscurePasswordText>((event, emit) {
      emit(
        RegisterState(
          isObscureConfirmPassword: state.isObscureConfirmPassword,
          isObscurePassword: !state.isObscurePassword,
          isMainBtnEnabled: state.isMainBtnEnabled,
          firstPassword: state.firstPassword,
          secondPassword: state.firstPassword,
        ),
      );
    });

    on<ObscureConfirmPasswordText>((event, emit) {
      emit(
        RegisterState(
          isObscureConfirmPassword: !state.isObscureConfirmPassword,
          isObscurePassword: state.isObscurePassword,
          isMainBtnEnabled: state.isMainBtnEnabled,
          firstPassword: state.firstPassword,
          secondPassword: state.firstPassword,
        ),
      );
    });

    on<UpdateFirstPassword>((event, emit) {
      emit(
        RegisterState(
          isObscureConfirmPassword: state.isObscureConfirmPassword,
          isObscurePassword: state.isObscurePassword,
          isMainBtnEnabled: state.isMainBtnEnabled,
          firstPassword: event.newPassword,
          secondPassword: state.secondPassword,
        ),
      );
    });

    on<UpdateSecondPassword>((event, emit) {
      emit(
        RegisterState(
          isObscureConfirmPassword: state.isObscureConfirmPassword,
          isObscurePassword: state.isObscurePassword,
          isMainBtnEnabled: state.isMainBtnEnabled,
          firstPassword: state.firstPassword,
          secondPassword: event.newPassword,
        ),
      );
    });
  }
}
