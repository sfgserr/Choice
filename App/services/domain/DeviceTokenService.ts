import {ChatUserService} from './ChatUserService.ts';
import messaging from '@react-native-firebase/messaging';
import DeviceInfo from 'react-native-device-info';

export class DeviceTokenService {
  private static isInit: boolean = false;
  private static token: string = '';

  private static chatUserService: ChatUserService;

  private constructor() {
  }


  public static async initialize(chatUserService: ChatUserService): Promise<void> {
    if (this.isInit) return;

    this.chatUserService = chatUserService;

    if (!messaging().isDeviceRegisteredForRemoteMessages)
      await messaging().registerDeviceForRemoteMessages();

    const token = await messaging().getToken();

    this.token = token;

    await this.chatUserService.addOrUpdateDevice(
      DeviceInfo.getDeviceId(),
      token,
    );

    messaging().onTokenRefresh(
      async () => {
        await this.chatUserService.addOrUpdateDevice(
          DeviceInfo.getDeviceId(),
          token,
        );

        this.token = token;
      },
    );

    this.isInit = true;
  }

  public static async addDevice() {
    if (this.isInit) {
      await this.chatUserService.addOrUpdateDevice(DeviceInfo.getDeviceId(), this.token);
    }
  }

  public static getToken() {
    if (!this.isInit) throw new Error();

    return this.token;
  }

  public static async removeDevice()  {
    await this.chatUserService.removeDevice(DeviceInfo.getDeviceId());
  }
}
