import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:equatable/equatable.dart';

part 'card_company_event.dart';
part 'card_company_state.dart';

class CardCompanyBloc extends Bloc<CardCompanyEvent, CardCompanyState> {
  CardCompanyBloc() : super(const CardCompanyFirstView(isMainBtnEnabled: false)) {
    on<ChangeCardCompanyView>((event, emit) {
      switch (event.newPage) {
        case 0:
          emit(const CardCompanyFirstView(isMainBtnEnabled: false));
        case 1:
          emit(const CardCompanySecondView(isMainBtnEnabled: false));
        case 2:
          emit(const CardCompanyThirdView(isMainBtnEnabled: false));
      }
    });

    on<EnableCardCompanyMainBtn>((event, emit) {
      if (state is CardCompanyFirstView) {
        emit(CardCompanyFirstView(isMainBtnEnabled: event.isMainBtnEnabled));
      } else if (state is CardCompanySecondView) {
        emit(CardCompanySecondView(isMainBtnEnabled: event.isMainBtnEnabled));
      } else if (state is CardCompanyThirdView) {
        emit(CardCompanyThirdView(isMainBtnEnabled: event.isMainBtnEnabled));
      }
    });
  }
}
