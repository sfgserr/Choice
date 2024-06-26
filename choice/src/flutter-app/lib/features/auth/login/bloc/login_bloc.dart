import 'package:choice/features/entry_point/bloc/auth_bloc.dart';
import 'package:choice/main.dart';
import 'package:choice/repositories/repositories/user_repository.dart';
import 'package:choice/repositories/storage/local_storage.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:get_it/get_it.dart';

part 'login_event.dart';

part 'login_state.dart';

class LoginBloc extends Bloc<LoginEvent, LoginState> {
  final UserRepository userRepository = UserRepository();
  final AuthBloc authBloc = GetIt.I<AuthBloc>();

  LoginBloc() : super(const LoginInitial()) {
    on<LoginTap>((event, emit) async {
      emit(LoginLoading(
        isObscurePassword: state.isObscurePassword,
        isGettingCode: state.isGettingCode,
        currentTabIndex: state.currentTabIndex,
        isLoginBtnEnabled: state.isLoginBtnEnabled,
      ));

      try {
        final Map<String, dynamic>? data =
            await userRepository.getUserByEmail(event.email);
        logger.i("Data = $data");
        if (data == null || data['password'] != event.password) {
          throw Exception('Неверный пароль!');
        }
        emit(const LoginInitial());
        authBloc.add(LoggedIn());
      } catch (error) {
        emit(LoginFailure(
          error: error.toString(),
          isObscurePassword: state.isObscurePassword,
          isGettingCode: state.isGettingCode,
          currentTabIndex: state.currentTabIndex,
          isLoginBtnEnabled: state.isLoginBtnEnabled,
        ));
      }
    });

    on<ResetOptions>((event, emit) {
      emit(const LoginInitial());
    });

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
