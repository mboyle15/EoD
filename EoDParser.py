import traceback
import csv
import time
import pyodbc
import logging

CSV_FILE = "z:\AS162.csv"
EventList = []
logging.basicConfig(format='%(asctime)s %(levelname)s - %(message)s',filename='EventLog.log',level=logging.DEBUG)

# open the csv file selecting rows with data only and add to the list along with timestamp
with open(CSV_FILE, 'r') as csvfile:
	reader = csv.reader(csvfile)
	for row in reader:
		if (len(row) > 3):
			EventList.append([time.strftime("%Y-%m-%d %H:%M:%S"),row[4],1])
	csvfile.close()
	
#for p in EventList: print p


#connect to the Database
conn = pyodbc.connect('Driver={SQL Server};'
					'Server=localhost;'
					'Database=EIBdata;'
					'uid=sa;pwd=cse2017-eod')
c = conn.cursor()

try:
	c.execute("INSERT INTO ElectricalRecords(RecordedDateTime,BuildingRecordId,Amount,Change) values(?,?,?,?)",
		EventList[1][0],
		EventList[1][2],
		EventList[1][1],
		EventList[2][1])
	
	c.execute("INSERT INTO NaturalGasRecords(RecordedDateTime,Amount,BuildingRecordId) values(?,?,?)",
		EventList[4][0],
		EventList[4][1],
		EventList[4][2])

	c.execute("INSERT INTO WaterRecords(RecordedDateTime,Amount,BuildingRecordId) values(?,?,?)",
		EventList[3][0],
		EventList[3][1],
		EventList[3][2])

	c.execute("INSERT INTO OutsideTempRecords(RecordedDateTime,Amount,BuildingRecordId) values(?,?,?)",
		EventList[0][0],
		EventList[0][1],
		EventList[0][2])

	logging.info('Data sucessfully inserted.')
except Exception, e:
	logging.error('Error inserting values into db')
	logging.error(traceback.format_exc())

conn.commit()
conn.close()
	
