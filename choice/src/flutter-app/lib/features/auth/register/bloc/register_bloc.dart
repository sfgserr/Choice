import 'package:choice/features/entry_point/bloc/auth_bloc.dart';
import 'package:choice/repositories/repositories/user_repository.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:get_it/get_it.dart';

part 'register_event.dart';

part 'register_state.dart';

class RegisterBloc extends Bloc<RegisterEvent, RegisterState> {
  final AuthBloc authBloc = GetIt.I<AuthBloc>();
  final UserRepository userRepository = UserRepository();

  RegisterBloc() : super(const RegisterInitial()) {
    on<SignUpTap>((event, emit) async {
      emit(RegisterLoading(
        isObscureConfirmPassword: state.isObscureConfirmPassword,
        isObscurePassword: state.isObscurePassword,
        isMainBtnEnabled: state.isMainBtnEnabled,
        firstPassword: state.firstPassword,
        isCompanyRegister: state.isCompanyRegister,
      ));

      try {
        if (state.isCompanyRegister) {
          /// sign up as a company
          final int? code = await userRepository.createNewCompany(
            event.email,
            event.password,
            event.name,
          );

          if (code != 200) {
            // TODO: recognise error using 'code'
            throw Exception('Ошибка при регистрации компании!');
          }
          authBloc.add(LoggedIn());
          emit(RegisterLoaded(
            email: event.email,
            name: event.name,
            isObscureConfirmPassword: state.isObscureConfirmPassword,
            isObscurePassword: state.isObscurePassword,
            isMainBtnEnabled: state.isMainBtnEnabled,
            firstPassword: state.firstPassword,
            isCompanyRegister: state.isCompanyRegister,
          ));


        } else {
          /// sign up as a client
          final int? code = await userRepository.createNewClient(
            event.email,
            event.password,
            event.name,
            event.surname!,
          );

          if (code == null || code != 200) {
            // TODO: recognise error using 'code'
            // TODO: and give to Exception some text
            throw Exception('Ошибка при регистрации клиента!');
          }
          authBloc.add(LoggedIn());
          emit(RegisterState(
            isObscureConfirmPassword: state.isObscureConfirmPassword,
            isObscurePassword: state.isObscurePassword,
            isMainBtnEnabled: state.isMainBtnEnabled,
            firstPassword: state.firstPassword,
            isCompanyRegister: state.isCompanyRegister,
          ));
        }
      } catch (error) {
        emit(RegisterFailure(
          error: error.toString(),
          isObscureConfirmPassword: state.isObscureConfirmPassword,
          isObscurePassword: state.isObscurePassword,
          isMainBtnEnabled: state.isMainBtnEnabled,
          firstPassword: state.firstPassword,
          isCompanyRegister: state.isCompanyRegister,
        ));
      }
    });

    on<CompanyRegister>((event, emit) {
      emit(RegisterState(
        isObscureConfirmPassword: state.isObscureConfirmPassword,
        isObscurePassword: state.isObscurePassword,
        isMainBtnEnabled: state.isMainBtnEnabled,
        firstPassword: state.firstPassword,
        isCompanyRegister: true,
      ));
    });

    on<ClientRegister>((event, emit) {
      emit(RegisterState(
        isObscureConfirmPassword: state.isObscureConfirmPassword,
        isObscurePassword: state.isObscurePassword,
        isMainBtnEnabled: state.isMainBtnEnabled,
        firstPassword: state.firstPassword,
        isCompanyRegister: false,
      ));
    });

    on<EnableMainBtn>((event, emit) {
      emit(
        RegisterState(
          isMainBtnEnabled: event.isMainBtnEnabled,
          isObscurePassword: state.isObscurePassword,
          isObscureConfirmPassword: state.isObscureConfirmPassword,
          firstPassword: state.firstPassword,
          isCompanyRegister: state.isCompanyRegister,
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
          isCompanyRegister: state.isCompanyRegister,
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
          isCompanyRegister: state.isCompanyRegister,
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
          isCompanyRegister: state.isCompanyRegister,
        ),
      );
    });
  }
}
