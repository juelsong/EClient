from check.crc16 import crc_ccitt_calc
from constants.constants import DATA_SOURCE_ENUM


class DataUploadStructure:
    def __init__(self):
        self.net_id = 0
        self.node_id = 0
        self.update_time_bytes = bytearray(8)
        self.mn_code = bytearray(16)
        self.data = []
        self.data_source = 'unidentified'


class ProtocolResult:
    def __init__(self):
        self.protocol_packages_to_target = []  # 传输给piping目标对象的数据包
        self.protocol_acknowledges = []  # 返回给发送者的数据包
        self.protocol_packages_to_io_board = []  # 发送给io_board的数据包
        self.remainder_array = bytearray(0)
        self.device_id = 0
        self.source_type = DATA_SOURCE_ENUM.undefined
        self.extra_info_dict = None
        self.is_valid = False


class MonitorFactorData:
    def __init__(self, max, min, average, current, flag):
        self.max = max
        self.min = min
        self.average = average
        self.current = current
        self.flag = flag

na001_protocol_length_min = 11


def na001_protocol_extract(data):
    err_structure = {
        'is_valid': False
    }

    if len(data) < na001_protocol_length_min:
        err_structure['error_message'] = 'data not enough'
        return err_structure

    data_structure = {
        'is_valid': False,
        'head': data[0],
        'command_type': data[1],
        'command_symbol': data[2],
        'command_control': data[3],
        'command_request': data[4:6],
        'payload_length': int.from_bytes(data[6:8], byteorder='big'),
    }

    if len(data) < (data_structure['payload_length'] + na001_protocol_length_min):
        err_structure['error_message'] = 'data length error'
        return err_structure

    if data_structure['payload_length'] == 0:
        data_structure['payload_data'] = bytes.fromhex('')
    else:
        data_structure['payload_data'] = data[8: 8 + data_structure['payload_length']]

    data_structure['crc'] = int.from_bytes(
        data[8 + data_structure['payload_length']: 10 + data_structure['payload_length']], byteorder='big')

    data_structure['tail'] = data[10 + data_structure['payload_length']]

    if data_structure['head'] != 0xAC:
        err_structure['error_message'] = 'head byte error'
        return err_structure
    elif data_structure['tail'] != 0xB1:
        err_structure['error_message'] = 'tail byte error'
        return err_structure
    else:
        crc_value = crc_ccitt_calc(data, 8 + data_structure['payload_length'])

        if crc_value == data_structure['crc']:
            data_structure['is_valid'] = True
            return data_structure
        else:
            err_structure['error_message'] = 'crc error'
            return err_structure


def na001_package(command_type, command_symbol, command_control, command_request, data_byte_array):
    """
    打包na001协议
    :param command_type:
    :param command_symbol:
    :param command_control:
    :param command_request:
    :param data_byte_array:
    :return:
    """
    packet = bytearray.fromhex('')
    packet.insert(0, 0xAC)
    packet.insert(1, command_type)
    packet.insert(2, command_symbol)
    packet.insert(3, command_control)
    packet += bytearray(command_request.to_bytes(2, byteorder='big'))
    packet += bytearray(len(data_byte_array).to_bytes(2, byteorder='big'))

    packet += data_byte_array

    crc16 = crc_ccitt_calc(packet, len(packet))

    packet += bytearray(crc16.to_bytes(2, byteorder='big'))
    packet.insert(len(packet), 0xB1)

    return packet
