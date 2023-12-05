import 'package:flutter_bloc/flutter_bloc.dart';
import 'input_widget_event.dart';
import 'input_widget_state.dart';

class InputWidgetBloc extends Bloc<InputWidgetEvent, InputWidgetState> {
  InputWidgetBloc() : super(InputWidgetInitial()) {
    on<SuffixTap>((event, emit) {
      emit(InputWidgetState(
        obscureText: !state.obscureText,
      ));
    });
  }
}
